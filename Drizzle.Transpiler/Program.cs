using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Drizzle.Lingo.Parser.Ast;
using Drizzle.Lingo.Runtime;
using Pidgin;

namespace Drizzle.Transpiler
{
    class Program
    {
        public static readonly HashSet<string> MovieScripts = new()
        {
            "testDraw",
            "stop",
            "spelrelarat",
            "ropeModel",
            "lvl",
            "levelRendering",
            "fiffigt",
            "TEdraw",
            "FILE"
        };

        public static readonly HashSet<string> ParentScripts = new()
        {
            "PNG_encode",
            "levelEdit_parentscript"
        };


        private static readonly string SourcesRoot = Path.Combine("..", "..", "..", "..", "LingoSource");
        private static readonly string SourcesDest = Path.Combine("..", "..", "..", "..", "Drizzle.Ported");
        private const string OutputNamespace = "Drizzle.Ported";

        static void Main(string[] args)
        {
            var scripts = Directory.GetFiles(SourcesRoot)
                .AsParallel()
                .Select(n =>
                {
                    using var reader = new StreamReader(n);
                    var script = LingoParser.Script.ParseOrThrow(reader);
                    var name = Path.GetFileNameWithoutExtension(n);
                    return (name, script);
                })
                .ToDictionary(n => n.name, n => n.script);

            var movieScripts = scripts.Where(kv => MovieScripts.Contains(kv.Key))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var parentScripts = scripts.Where(kv => ParentScripts.Contains(kv.Key));
            var behaviorScripts = scripts.Except(movieScripts).Except(parentScripts);

            var movieHandlers = movieScripts.Values
                .SelectMany(s => s.Nodes)
                .OfType<AstNode.Handler>()
                .Select(h => h.Name)
                .ToHashSet();

            var globalContext = new GlobalContext(movieHandlers);

            OutputMovieScripts(scripts, globalContext);
            OutputMovieGlobals(globalContext);
        }

        private static void OutputMovieGlobals(GlobalContext globalContext)
        {
            var path = Path.Combine(SourcesDest, "Movie._globals.cs");
            using var file = new StreamWriter(path);

            WriteFileHeader(file);
            file.WriteLine();
            file.WriteLine($"//\n// Movie globals\n//");
            file.WriteLine("public sealed partial class Movie {");

            foreach (var glob in globalContext.AllGlobals)
            {
                file.WriteLine($"public dynamic global_{glob};");
            }

            file.WriteLine("}\n}");
        }

        private static void OutputMovieScripts(
            IEnumerable<KeyValuePair<string, AstNode.Script>> scripts,
            GlobalContext ctx)
        {
            foreach (var (name, script) in scripts.Where(kv => MovieScripts.Contains(kv.Key)))
            {
                var path = Path.Combine(SourcesDest, $"Movie.{name}.cs");
                using var file = new StreamWriter(path);

                OutputSingleMovieScript(name, script, file, ctx);
            }
        }

        private static void WriteFileHeader(TextWriter writer)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using Drizzle.Lingo.Runtime;");
            writer.WriteLine($"namespace {OutputNamespace} {{");
        }

        private static void OutputSingleMovieScript(
            string name,
            AstNode.Script script,
            TextWriter writer,
            GlobalContext ctx)
        {
            WriteFileHeader(writer);
            writer.WriteLine($"//\n// Movie script: {name}\n//");
            writer.WriteLine("public sealed partial class Movie {");

            var allGlobals = script.Nodes.OfType<AstNode.Global>().SelectMany(g => g.Identifiers).ToHashSet();
            var allHandlers = script.Nodes.OfType<AstNode.Handler>().Select(h => h.Name).ToHashSet();
            var scriptContext = new ScriptContext(ctx, allGlobals, allHandlers);

            foreach (var handler in script.Nodes.OfType<AstNode.Handler>())
            {
                // Have to write into a temporary buffer because we need to pre-declare all variables.
                var tempWriter = new StringWriter();
                var handlerContext = new HandlerContext(scriptContext, tempWriter);
                handlerContext.Locals.UnionWith(handler.Parameters);

                var paramsText = string.Join(',', handler.Parameters.Select(p => $"dynamic {p}"));
                writer.WriteLine($"public dynamic {handler.Name.ToLower()}({paramsText}) {{");

                try
                {
                    WriteStatementBlock(handler.Body, handlerContext);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to write handler {handler.Name}:\n{e}");
                    writer.WriteLine("throw new System.NotImplementedException(\"Compilation failed\");\n}");
                    continue;
                }

                foreach (var local in handlerContext.DeclaredLocals)
                {
                    writer.WriteLine($"dynamic {local} = null;");
                }

                writer.WriteLine(tempWriter.GetStringBuilder());

                // Handler end.
                writer.WriteLine("}");
            }

            ctx.AllGlobals.UnionWith(allGlobals);

            // End class and namespace.
            writer.WriteLine("}\n}");
        }

        private static void WriteStatementBlock(AstNode.StatementBlock node, HandlerContext ctx)
        {
            foreach (var statement in node.Statements)
            {
                WriteStatement(statement, ctx);
            }
        }

        private static void WriteStatement(AstNode.Base node, HandlerContext ctx)
        {
            switch (node)
            {
                case AstNode.Assignment ass:
                    WriteAssignment(ass, ctx);
                    break;
                case AstNode.Return ret:
                    WriteReturn(ret, ctx);
                    break;
                default:
                    var exprValue = WriteExpression(node, ctx);
                    ctx.Writer.Write(exprValue);
                    ctx.Writer.WriteLine(';');
                    break;
            }
        }

        private static void WriteReturn(AstNode.Return ret, HandlerContext ctx)
        {
            if (ret.Value != null)
            {
                var value = WriteExpression(ret.Value, ctx);
                ctx.Writer.WriteLine($"return {value};");
            }
            else
            {
                ctx.Writer.WriteLine($"return;");
            }
        }

        private static void WriteAssignment(AstNode.Assignment node, HandlerContext ctx)
        {
            if (node.Assigned is AstNode.VariableName simpleTarget)
            {
                // Define local variable if necessary.
                if (!ctx.Parent.AllGlobals.Contains(simpleTarget.Name))
                {
                    // Local variable, not global
                    // Make sure it's not a parameter though.
                    if (ctx.Locals.Add(simpleTarget.Name))
                        ctx.DeclaredLocals.Add(simpleTarget.Name);
                }
            }

            var lhs = WriteExpression(node.Assigned, ctx);
            var rhs = WriteExpression(node.Value, ctx);

            ctx.Writer.WriteLine($"{lhs} = {rhs};");
        }

        private static string WriteExpression(AstNode.Base node, HandlerContext ctx)
        {
            return node switch
            {
                AstNode.BinaryOperator binaryOperator => WriteBinaryOperator(binaryOperator, ctx),
                AstNode.Constant constant => WriteConstant(constant, ctx),
                AstNode.Decimal @decimal => WriteDecimal(@decimal, ctx),
                AstNode.GlobalCall globalCall => WriteGlobalCall(globalCall, ctx),
                AstNode.Integer integer => WriteInteger(integer, ctx),
                AstNode.List list => WriteList(list, ctx),
                AstNode.MemberCall memberCall => WriteMemberCall(memberCall, ctx),
                AstNode.MemberIndex memberIndex => WriteMemberIndex(memberIndex, ctx),
                AstNode.MemberProp memberProp => WriteMemberProp(memberProp, ctx),
                AstNode.MemberSlice memberSlice => WriteMemberSlice(memberSlice, ctx),
                AstNode.NewCastLib newCastLib => WriteNewCastLib(newCastLib, ctx),
                AstNode.NewScript newScript => WriteNewScript(newScript, ctx),
                AstNode.ParameterList parameterList => throw new NotImplementedException(),
                AstNode.PropertyList propertyList => throw new NotImplementedException(),
                AstNode.String str => WriteString(str, ctx),
                AstNode.Symbol symbol => WriteSymbol(symbol, ctx),
                AstNode.The the => throw new NotImplementedException(),
                AstNode.TheNumberOf theNumberOf => throw new NotImplementedException(),
                AstNode.TheNumberOfLines theNumberOfLines => throw new NotImplementedException(),
                AstNode.ThingOf thingOf => throw new NotImplementedException(),
                AstNode.UnaryOperator unaryOperator => WriteUnaryOperator(unaryOperator, ctx),
                AstNode.VariableName variableName => WriteVariableName(variableName, ctx),
                _ => throw new NotSupportedException($"{node.GetType()} is not a supported expression type")
            };
        }

        private static string WriteVariableName(AstNode.VariableName variableName, HandlerContext ctx)
        {
            var name = variableName.Name;
            if (ctx.Locals.Contains(name))
                return name;

            if (ctx.Parent.AllGlobals.Contains(name))
                return $"_movieScript.global_{name}";

            return $"_global.{name}";
        }

        private static string WriteUnaryOperator(AstNode.UnaryOperator unaryOperator, HandlerContext ctx)
        {
            var op = unaryOperator.Type == AstNode.UnaryOperatorType.Negate ? "-" : "!";
            var expr = WriteExpression(unaryOperator.Expression, ctx);

            return $"{op}{expr}";
        }

        private static string WriteSymbol(AstNode.Symbol node, HandlerContext ctx)
        {
            return $"new LingoSymbol(\"{node.Value}\")";
        }

        private static string WriteString(AstNode.String node, HandlerContext ctx)
        {
            var escaped = node.Value.Replace("\"", "\"\"");
            return $"@\"{escaped}\"";
        }

        private static string WriteNewScript(AstNode.NewScript node, HandlerContext ctx)
        {
            var wrapListNode = new AstNode.List(node.Args);
            return WriteGlobalCall("new_script", ctx, node.Type, wrapListNode);
        }

        private static string WriteNewCastLib(AstNode.NewCastLib node, HandlerContext ctx)
        {
            return WriteGlobalCall("new_castlib", ctx, node.Type, node.CastLib);
        }

        private static string WriteMemberSlice(AstNode.MemberSlice node, HandlerContext ctx)
        {
            return WriteGlobalCall("slice_helper", ctx, node.Expression, node.Start, node.End);
        }

        private static string WriteMemberProp(AstNode.MemberProp node, HandlerContext ctx)
        {
            var child = WriteExpression(node.Expression, ctx);
            return $"{child}.{node.Property}";
        }

        private static string WriteMemberIndex(AstNode.MemberIndex node, HandlerContext ctx)
        {
            var child = WriteExpression(node.Expression, ctx);
            var idx = WriteExpression(node.Index, ctx);
            return $"{child}[{idx}]";
        }

        private static string WriteMemberCall(AstNode.MemberCall node, HandlerContext ctx)
        {
            var child = WriteExpression(node.Expression, ctx);
            var args = node.Parameters.Select(v => WriteExpression(v, ctx));
            return $"{child}.{node.Name}({string.Join(',', args)})";
        }

        private static string WriteList(AstNode.List node, HandlerContext ctx)
        {
            var args = node.Values.Select(v => WriteExpression(v, ctx));
            return $"new LingoList(new dynamic[] {{ {string.Join(',', args)} }})";
        }

        private static string WriteInteger(AstNode.Integer node, HandlerContext ctx)
        {
            // That was easy.
            return node.ToString();
        }

        private static string WriteGlobalCall(AstNode.GlobalCall node, HandlerContext ctx)
        {
            var args = node.Arguments.Select(a => WriteExpression(a, ctx));
            if (ctx.Parent.AllHandlers.Contains(node.Name))
            {
                // Local call
                return $"{node.Name}({string.Join(',', args)})";
            }

            if (ctx.Parent.Parent.MovieHandlers.Contains(node.Name))
            {
                // Movie script call
                return $"_movieScript.{node.Name}({string.Join(',', args)})";
            }

            return WriteGlobalCall(node.Name, ctx, args);
        }

        private static string WriteDecimal(AstNode.Decimal node, HandlerContext ctx)
        {
            return $"new LingoGlobal({node.Value.Value:R})";
        }

        private static string WriteConstant(AstNode.Constant node, HandlerContext ctx)
        {
            return $"LingoGlobal.{node.Name.ToUpper()}";
        }

        private static string WriteBinaryOperator(AstNode.BinaryOperator node, HandlerContext ctx)
        {
            // Operators that need to map to special functions.
            switch (node.Type)
            {
                case AstNode.BinaryOperatorType.Contains:
                    return WriteGlobalCall("contains", ctx, node.Left, node.Right);
                case AstNode.BinaryOperatorType.Starts:
                    return WriteGlobalCall("starts", ctx, node.Left, node.Right);
                case AstNode.BinaryOperatorType.Concat:
                    return WriteGlobalCall("concat", ctx, node.Left, node.Right);
                case AstNode.BinaryOperatorType.ConcatSpace:
                    return WriteGlobalCall("concat_space", ctx, node.Left, node.Right);
            }

            var sb = new StringBuilder();

            var op = node.Type switch
            {
                AstNode.BinaryOperatorType.LessThan => "<",
                AstNode.BinaryOperatorType.LessThanOrEqual => "<=",
                AstNode.BinaryOperatorType.NotEqual => "!=",
                AstNode.BinaryOperatorType.Equal => "==",
                AstNode.BinaryOperatorType.GreaterThan => ">",
                AstNode.BinaryOperatorType.GreaterThanOrEqual => ">=",
                AstNode.BinaryOperatorType.Add => "+",
                AstNode.BinaryOperatorType.Multiply => "*",
                AstNode.BinaryOperatorType.Divide => "/",
                AstNode.BinaryOperatorType.And => "&&",
                AstNode.BinaryOperatorType.Or => "||",
                AstNode.BinaryOperatorType.Mod => "%",
                AstNode.BinaryOperatorType.Subtract => "-",
                _ => throw new ArgumentOutOfRangeException()
            };

            sb.Append('(');
            sb.Append(WriteExpression(node.Left, ctx));
            sb.Append(op);
            sb.Append(WriteExpression(node.Right, ctx));
            sb.Append(')');

            return sb.ToString();
        }

        private static string WriteGlobalCall(string name, HandlerContext ctx, params AstNode.Base[] args)
        {
            return WriteGlobalCall(name, ctx, args.Select(a => WriteExpression(a, ctx)));
        }

        private static string WriteGlobalCall(string name, HandlerContext ctx, IEnumerable<string> args)
        {
            var method = typeof(LingoGlobal).GetMember(name,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            var isStatic = method.Any(m => m is MethodInfo {IsStatic: true});

            var sb = new StringBuilder();

            sb.Append(isStatic ? "LingoGlobal." : "_global.");
            sb.Append(name.ToLower());
            sb.Append('(');
            sb.Append(string.Join(',', args));
            sb.Append(')');

            return sb.ToString();
        }

        private sealed class GlobalContext
        {
            public GlobalContext(HashSet<string> movieHandlers)
            {
                MovieHandlers = movieHandlers;
            }

            public HashSet<string> AllGlobals { get; } = new();
            public HashSet<string> MovieHandlers { get; }
        }

        private sealed class ScriptContext
        {
            public GlobalContext Parent { get; }
            public HashSet<string> AllGlobals { get; }
            public HashSet<string> AllHandlers { get; }

            public ScriptContext(GlobalContext parent, HashSet<string> allGlobals, HashSet<string> allHandlers)
            {
                Parent = parent;
                AllGlobals = allGlobals;
                AllHandlers = allHandlers;
            }
        }

        private sealed class HandlerContext
        {
            public HandlerContext(ScriptContext parent, TextWriter writer)
            {
                Parent = parent;
                Writer = writer;
            }

            public HashSet<string> Locals { get; } = new();
            public HashSet<string> DeclaredLocals { get; } = new();
            public ScriptContext Parent { get; }
            public TextWriter Writer { get; }
        }
    }
}
