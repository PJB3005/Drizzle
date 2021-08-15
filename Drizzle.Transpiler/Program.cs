using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Drizzle.Lingo.Runtime;
using Drizzle.Lingo.Runtime.Parser;
using Pidgin;

namespace Drizzle.Transpiler
{
    internal static class Program
    {
        public static readonly HashSet<string> MovieScripts = new()
        {
            "testDraw",
            "stop",
            "spelrelaterat",
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

        private static readonly Dictionary<string, ScriptQuirks> Quirks = new()
        {
            ["fiffigt"] = new ScriptQuirks
            {
                BlackListHandlers = { "giveHitSurf" }
            },
            ["levelRendering"] = new ScriptQuirks
            {
                OverloadParamCounts =
                {
                    ("drawATileTile", 4),
                    ("drawATileTile", 5),
                }
            },
            ["spelrelaterat"] = new ScriptQuirks
            {
                OverloadParamCounts =
                {
                    ("copyPixelsToEffectColor", 6),
                    ("seedForTile", 1),
                }
            },
            ["lvl"] = new ScriptQuirks
            {
                OverloadParamCounts =
                {
                    ("lvleditdraw", 2),
                    ("drawshortcutsimg", 2)
                }
            },
            ["PNG_encode"] = new ScriptQuirks
            {
                BlackListHandlers =
                {
                    "png_encode", "writeChunk", "writeBytes", "writeInt", "gzcompress", "writeCRC", "lingo_crc32",
                    "bitShift8", "xtraPresent"
                }
            },
            ["TEdraw"] = new ScriptQuirks
            {
                OverloadParamCounts =
                {
                    ("tedraw", 2)
                }
            },
            ["loadLevel"] = new ScriptQuirks
            {
                OverloadParamCounts =
                {
                    ("loadlevel", 1)
                }
            }
        };

        private const string OutputNamespace = "Drizzle.Ported";

        private static void Main(string[] args)
        {
            var sourcesRoot = Path.Combine("..", "..", "..", "..", "LingoSource");
            var sourcesDest = Path.Combine("..", "..", "..", "..", "Drizzle.Ported", "Translated");

            if (args.Length == 2)
            {
                sourcesRoot = args[0];
                sourcesDest = args[1];
            }

            var scripts = Directory.GetFiles(sourcesRoot, "*.lingo")
                .AsParallel()
                .Select(n =>
                {
                    using var reader = new StreamReader(n);
                    var script = LingoParser.Script.Parse(reader);
                    if (!script.Success)
                    {
                        throw new Exception($"Parsing failed in file {n}\n{script.Error!.RenderErrorMessage()}");
                    }

                    var name = Path.GetFileNameWithoutExtension(n);
                    return (name, script: script.Value);
                })
                .ToDictionary(n => n.name, n => n.script);

            var movieScripts = scripts.Where(kv => MovieScripts.Contains(kv.Key))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var parentScripts = scripts.Where(kv => ParentScripts.Contains(kv.Key))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var behaviorScripts = scripts.Except(movieScripts).Except(parentScripts)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            var movieHandlers = movieScripts.Values
                .SelectMany(s => s.Nodes)
                .OfType<AstNode.Handler>()
                .Select(h => h.Name)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

            var globalContext = new GlobalContext(movieHandlers, sourcesDest);

            OutputMovieScripts(movieScripts, globalContext);
            OutputParentScripts(parentScripts, globalContext);
            OutputBehaviorScripts(behaviorScripts, globalContext);

            OutputMovieGlobals(globalContext);
        }

        private static void OutputBehaviorScripts(
            IEnumerable<KeyValuePair<string, AstNode.Script>> scripts,
            GlobalContext ctx)
        {
            foreach (var (name, script) in scripts)
            {
                var path = Path.Combine(ctx.SourcesDest, $"Behavior.{name}.cs");
                using var file = new StreamWriter(path);

                OutputSingleBehaviorScript(name, script, file, ctx);
            }
        }

        private static void OutputSingleBehaviorScript(
            string name,
            AstNode.Script script,
            TextWriter writer,
            GlobalContext ctx)
        {
            WriteFileHeader(writer);
            writer.WriteLine($"//\n// Behavior script: {name}\n//");
            writer.WriteLine("[BehaviorScript]");
            writer.WriteLine($"public sealed partial class {name} : LingoBehaviorScript {{");

            EmitScriptBody(name, script, writer, ctx, isMovieScript: false);

            // End class and namespace.
            writer.WriteLine("}\n}");
        }

        private static void OutputParentScripts(
            IEnumerable<KeyValuePair<string, AstNode.Script>> scripts,
            GlobalContext ctx)
        {
            foreach (var (name, script) in scripts)
            {
                var path = Path.Combine(ctx.SourcesDest, $"Parent.{name}.cs");
                using var file = new StreamWriter(path);

                OutputSingleParentScript(name, script, file, ctx);
            }
        }

        private static void OutputSingleParentScript(
            string name,
            AstNode.Script script,
            TextWriter writer,
            GlobalContext ctx)
        {
            WriteFileHeader(writer);
            writer.WriteLine($"//\n// Parent script: {name}\n//");
            writer.WriteLine("[ParentScript]");
            writer.WriteLine($"public sealed partial class {name} : LingoParentScript {{");

            EmitScriptBody(name, script, writer, ctx, isMovieScript: false);

            // End class and namespace.
            writer.WriteLine("}\n}");
        }

        private static void OutputMovieGlobals(GlobalContext ctx)
        {
            var path = Path.Combine(ctx.SourcesDest, "Movie._globals.cs");
            using var file = new StreamWriter(path);

            WriteFileHeader(file);
            file.WriteLine();
            file.WriteLine($"//\n// Movie globals\n//");
            file.WriteLine("public sealed partial class MovieScript {");

            foreach (var glob in ctx.AllGlobals)
            {
                file.WriteLine($"[LingoGlobal] public dynamic {glob};");
            }

            file.WriteLine("}\n}");
        }

        private static void OutputMovieScripts(
            IEnumerable<KeyValuePair<string, AstNode.Script>> scripts,
            GlobalContext ctx)
        {
            foreach (var (name, script) in scripts)
            {
                var path = Path.Combine(ctx.SourcesDest, $"Movie.{name}.cs");
                Directory.CreateDirectory(ctx.SourcesDest);
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
            writer.WriteLine("public sealed partial class MovieScript {");

            EmitScriptBody(name, script, writer, ctx, isMovieScript: true);

            // End class and namespace.
            writer.WriteLine("}\n}");
        }

        private static void EmitScriptBody(
            string name,
            AstNode.Script script,
            TextWriter writer,
            GlobalContext ctx,
            bool isMovieScript)
        {
            var allGlobals = script.Nodes.OfType<AstNode.Global>().SelectMany(g => g.Identifiers)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);
            var allHandlers = script.Nodes.OfType<AstNode.Handler>().Select(h => h.Name)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);
            var scriptContext = new ScriptContext(ctx, allGlobals, allHandlers, isMovieScript);

            ctx.AllGlobals.UnionWith(allGlobals);

            var props = new HashSet<string>();
            foreach (var prop in script.Nodes.OfType<AstNode.Property>().SelectMany(p => p.Identifiers))
            {
                props.Add(prop);
                writer.WriteLine($"[LingoProperty] public dynamic {prop};");
            }

            var quirks = Quirks.GetValueOrDefault(name);

            foreach (var handler in script.Nodes.OfType<AstNode.Handler>())
            {
                if (quirks?.BlackListHandlers.Contains(handler.Name) ?? false)
                    continue;

                // Have to write into a temporary buffer because we need to pre-declare all variables.
                var tempWriter = new StringWriter();
                var handlerContext = new HandlerContext(scriptContext, handler.Name, tempWriter);
                handlerContext.Locals.UnionWith(handler.Parameters);
                handlerContext.Locals.UnionWith(props);

                var paramsList = handler.Parameters.ToList();
                if (paramsList.Count > 0 && paramsList[0] == "me")
                    paramsList.RemoveAt(0);

                var paramsText = string.Join(',', paramsList.Select(p => $"dynamic {p.ToLower()}"));
                var handlerLower = handler.Name.ToLower();
                writer.WriteLine($"public dynamic {WriteSanitizeIdentifier(handlerLower)}({paramsText}) {{");

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
                    writer.WriteLine($"dynamic {local.ToLower()} = null;");
                }

                writer.WriteLine(tempWriter.GetStringBuilder());

                if (handler.Body.Statements.Length == 0 || handler.Body.Statements[^1] is not AstNode.Return)
                    writer.WriteLine("return null;");

                // Handler end.
                writer.WriteLine("}");
            }

            GenerateParamCountOverloads(writer, quirks, script);
        }

        private static void GenerateParamCountOverloads(
            TextWriter writer,
            ScriptQuirks? quirks,
            AstNode.Script script)
        {
            if (quirks == null)
                return;

            var handlers = script.Nodes.OfType<AstNode.Handler>()
                .ToDictionary(
                    h => h.Name,
                    h => h.Parameters.Count(p => p != "me"),
                    StringComparer.InvariantCultureIgnoreCase);

            foreach (var (h, count) in quirks.OverloadParamCounts)
            {
                var toLower = h.ToLower();
                var parameters = Enumerable.Range(1, count).Select(i => $"p{i}").ToArray();

                var overloadParams = string.Join(", ",
                    parameters.Take(count).Select(p => $"dynamic {p}"));

                writer.WriteLine($"public dynamic {toLower}({overloadParams}) {{");

                var nulls = string.Join(", ", Enumerable.Repeat("null", handlers[toLower] - count));

                overloadParams = string.Concat(parameters.Take(count).Select(p => $"{p}, "));

                writer.WriteLine($"return {toLower}({overloadParams}{nulls});");

                writer.WriteLine("}");
            }
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
                case AstNode.ExitRepeat exitRepeat:
                    WriteExitRepeat(exitRepeat, ctx);
                    break;
                case AstNode.Case @case:
                    WriteCase(@case, ctx);
                    break;
                case AstNode.RepeatWhile repeatWhile:
                    WriteRepeatWhile(repeatWhile, ctx);
                    break;
                case AstNode.RepeatWithCounter repeatWithCounter:
                    WriteRepeatWithCounter(repeatWithCounter, ctx);
                    break;
                case AstNode.RepeatWithList repeatWithList:
                    WriteRepeatWithList(repeatWithList, ctx);
                    break;
                case AstNode.If @if:
                    WriteIf(@if, ctx);
                    break;
                case AstNode.PutInto putInto:
                    WritePutInto(putInto, ctx);
                    break;
                case AstNode.Global global:
                    WriteGlobal(global, ctx);
                    break;
                case AstNode.Property prop:

                    break;
                default:
                    if (node is AstNode.VariableName
                        or AstNode.MemberProp or AstNode.MemberIndex or AstNode.BinaryOperator or AstNode.UnaryOperator
                        or AstNode.String or AstNode.Number or AstNode.Symbol or AstNode.List or
                        AstNode.Property)
                    {
                        Console.WriteLine($"Warning: {ctx.Name} has loose expression {node}");
                    }

                    var exprValue = WriteExpression(node, ctx);
                    ctx.Writer.Write(exprValue);
                    ctx.Writer.WriteLine(';');
                    break;
            }
        }

        private static void WriteGlobal(AstNode.Global node, HandlerContext ctx)
        {
            foreach (var declared in node.Identifiers.Except(ctx.Globals))
            {
                // These get handled when locals are declared.
                /*ctx.DeclaredGlobals.Add(declared);
                ctx.Locals.Add(declared);*/
                ctx.Globals.Add(declared);
                ctx.Parent.Parent.AllGlobals.Add(declared);
            }
        }

        private static void WritePutInto(AstNode.PutInto node, HandlerContext ctx)
        {
            if (node.Type != AstNode.PutType.After)
                throw new NotSupportedException();

            var coll = WriteExpression(node.Collection, ctx);
            var expr = WriteExpression(node.Expression, ctx);
            ctx.Writer.WriteLine($"{coll} += {expr}.ToString();");
        }

        private static void WriteIf(AstNode.If node, HandlerContext ctx)
        {
            var exprParams = new ExpressionParams { WantBool = true };
            var cond = WriteExpression(node.Condition, ctx, exprParams);

            ctx.Writer.WriteLine(exprParams.BoolGranted
                ? $"if ({cond}) {{"
                : $"if (LingoGlobal.ToBool({cond})) {{");

            WriteStatementBlock(node.Statements, ctx);
            ctx.Writer.WriteLine('}');

            if (node.Else != null && node.Else.Statements.Length > 0)
            {
                var elseIf = node.Else.Statements.Length == 1 && node.Else.Statements[0] is AstNode.If;

                ctx.Writer.Write("else ");
                // If the else clause is another if it's an else-if chain
                // and we forego the braces around the else.
                if (!elseIf)
                    ctx.Writer.WriteLine('{');

                WriteStatementBlock(node.Else, ctx);
                if (!elseIf)
                    ctx.Writer.WriteLine('}');
            }
        }

        private static void WriteRepeatWithList(AstNode.RepeatWithList node, HandlerContext ctx)
        {
            var expr = WriteExpression(node.ListExpr, ctx);
            var name = node.Variable;
            var loopTmp = $"tmp_{name}";
            ctx.Writer.WriteLine($"foreach (dynamic {loopTmp} in {expr}) {{");

            MakeLoopTmp(ctx, name, loopTmp);
            WriteStatementBlock(node.Block, ctx);

            ctx.Writer.WriteLine("}");
        }

        private static void WriteRepeatWithCounter(AstNode.RepeatWithCounter node, HandlerContext ctx)
        {
            var start = WriteExpression(node.Start, ctx);
            var end = WriteExpression(node.Finish, ctx);
            var name = node.Variable;
            var loopTmp = $"tmp_{name}";

            ctx.Writer.WriteLine($"for (int {loopTmp} = (int) ({start}); {loopTmp} <= {end}; {loopTmp}++) {{");

            MakeLoopTmp(ctx, name, $"(LingoNumber){loopTmp}");
            WriteStatementBlock(node.Block, ctx);

            ctx.Writer.WriteLine("}");

            // ctx.LoopTempIdx--;
        }

        private static void MakeLoopTmp(HandlerContext ctx, string name, string loopTmp)
        {
            if (!IsGlobal(name, ctx, out _) && ctx.Locals.Add(name))
                ctx.DeclaredLocals.Add(name);

            ctx.Writer.WriteLine($"{WriteVariableNameCore(name, ctx)} = {loopTmp};");
        }

        private static void WriteRepeatWhile(AstNode.RepeatWhile node, HandlerContext ctx)
        {
            var exprParams = new ExpressionParams { WantBool = true };
            var expr = WriteExpression(node.Condition, ctx);

            ctx.Writer.WriteLine(exprParams.BoolGranted
                ? $"while ({expr}) {{"
                : $"while (LingoGlobal.ToBool({expr})) {{");

            WriteStatementBlock(node.Block, ctx);

            ctx.Writer.WriteLine('}');
        }

        private static void WriteCase(AstNode.Case node, HandlerContext ctx)
        {
            // Check if all expressions are literal.
            // If so we can translate it as a switch statement.
            var literals = node.Cases.SelectMany(c => c.exprs).All(e => e is AstNode.Constant or AstNode.String);

            var allInts = node.Cases.All(c => c.exprs.All(e => e is AstNode.Number { Value: { IsDecimal: false } }));
            literals |= allInts;

            if (literals)
            {
                ctx.Writer.Write("switch (");
                if (allInts)
                    ctx.Writer.Write("(int)");
                ctx.Writer.Write(WriteExpression(node.Expression, ctx));
                // If this is a string switch, .ToLower() it and switch on lowercase values.
                // to avoid any case problems.
                if (node.Cases.Length > 0 && node.Cases[0].exprs[0] is AstNode.String)
                    ctx.Writer.Write(".ToLowerInvariant()");
                ctx.Writer.WriteLine(") {");

                foreach (var (exprs, block) in node.Cases)
                {
                    foreach (var expr in exprs)
                    {
                        ctx.Writer.Write("case ");
                        if (expr is AstNode.String str)
                            ctx.Writer.Write(DoWriteString(str.Value.ToLowerInvariant()));
                        else if (expr is AstNode.Number num)
                        {
                            Debug.Assert(allInts);
                            ctx.Writer.Write(num.Value.ToString());
                        }
                        else
                            ctx.Writer.Write(WriteExpression(expr, ctx));
                        ctx.Writer.WriteLine(':');
                    }

                    WriteStatementBlock(block, ctx);

                    ctx.Writer.WriteLine("break;");
                }

                if (node.Otherwise != null)
                {
                    ctx.Writer.WriteLine("default:");
                    WriteStatementBlock(node.Otherwise, ctx);
                    ctx.Writer.WriteLine("break;");
                }

                ctx.Writer.WriteLine("}");
            }
            else
            {
                // Will have to be translated as if-else chain.
                throw new NotImplementedException();
            }
        }

        private static void WriteExitRepeat(AstNode.ExitRepeat node, HandlerContext ctx)
        {
            ctx.Writer.WriteLine("break;");
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
                ctx.Writer.WriteLine($"return null;");
            }
        }

        private static void WriteAssignment(AstNode.Assignment node, HandlerContext ctx)
        {
            if (node.Assigned is AstNode.VariableName simpleTarget)
            {
                var name = simpleTarget.Name.ToLower();
                // Define local variable if necessary.
                if (!IsGlobal(name, ctx, out _))
                {
                    // Local variable, not global
                    // Make sure it's not a parameter though.
                    if (ctx.Locals.Add(name))
                        ctx.DeclaredLocals.Add(name);
                }
            }

            var lhs = WriteExpression(node.Assigned, ctx);
            var rhs = WriteExpression(node.Value, ctx);

            ctx.Writer.WriteLine($"{lhs} = {rhs};");
        }

        private static string WriteExpression(AstNode.Base node, HandlerContext ctx, ExpressionParams? param = null)
        {
            return node switch
            {
                AstNode.BinaryOperator binaryOperator => WriteBinaryOperator(binaryOperator, ctx, param),
                AstNode.Constant constant => WriteConstant(constant, ctx),
                AstNode.Number number => WriteNumber(number, ctx),
                AstNode.GlobalCall globalCall => WriteGlobalCall(globalCall, ctx),
                AstNode.List list => WriteList(list, ctx),
                AstNode.MemberCall memberCall => WriteMemberCall(memberCall, ctx),
                AstNode.MemberIndex memberIndex => WriteMemberIndex(memberIndex, ctx),
                AstNode.MemberProp memberProp => WriteMemberProp(memberProp, ctx),
                AstNode.MemberSlice memberSlice => WriteMemberSlice(memberSlice, ctx),
                AstNode.NewCastLib newCastLib => WriteNewCastLib(newCastLib, ctx),
                AstNode.NewScript newScript => WriteNewScript(newScript, ctx),
                AstNode.PropertyList propertyList => WritePropertyList(propertyList, ctx),
                AstNode.String str => WriteString(str, ctx),
                AstNode.Symbol symbol => WriteSymbol(symbol, ctx),
                AstNode.The the => WriteThe(the, ctx),
                AstNode.TheNumberOf theNumberOf => WriteTheNumberOf(theNumberOf, ctx),
                AstNode.TheNumberOfLines theNumberOfLines => WriteTheNumberOfLines(theNumberOfLines, ctx),
                AstNode.ThingOf thingOf => WriteThingOf(thingOf, ctx),
                AstNode.UnaryOperator unaryOperator => WriteUnaryOperator(unaryOperator, ctx, param),
                AstNode.VariableName variableName => WriteVariableName(variableName, ctx),
                _ => throw new NotSupportedException($"{node.GetType()} is not a supported expression type")
            };
        }

        private static string WriteThingOf(AstNode.ThingOf thingOf, HandlerContext ctx)
        {
            var helper = thingOf.Type switch
            {
                AstNode.ThingOfType.Item => "itemof_helper",
                AstNode.ThingOfType.Line => "lineof_helper",
                AstNode.ThingOfType.Char => "charof_helper",
                _ => throw new ArgumentOutOfRangeException()
            };

            return WriteGlobalCall(helper, ctx, thingOf.Index, thingOf.Collection);
        }

        private static string WriteTheNumberOfLines(AstNode.TheNumberOfLines node, HandlerContext ctx)
        {
            return WriteGlobalCall("thenumberoflines_helper", ctx, node.Text);
        }

        private static string WriteTheNumberOf(AstNode.TheNumberOf node, HandlerContext ctx)
        {
            return WriteGlobalCall("thenumberof_helper", ctx, node.Expr);
        }

        private static string WriteThe(AstNode.The node, HandlerContext ctx)
        {
            return $"_global.the_{node.Name}";
        }

        private static string WritePropertyList(AstNode.PropertyList node, HandlerContext ctx)
        {
            if (node.Values.Length == 0)
                return "new LingoPropertyList()";

            var sb = new StringBuilder();
            sb.Append("new LingoPropertyList {");
            var first = true;
            foreach (var (k, v) in node.Values)
            {
                if (!first)
                    sb.Append(',');

                first = false;
                var kExpr = WriteExpression(k, ctx);
                var vExpr = WriteExpression(v, ctx);
                sb.Append('[');
                sb.Append(kExpr);
                sb.Append("] = ");
                sb.Append(vExpr);
            }

            sb.Append("}");

            return sb.ToString();
        }

        private static string WriteVariableName(AstNode.VariableName variableName, HandlerContext ctx)
        {
            return WriteVariableNameCore(variableName.Name, ctx);
        }

        private static string WriteVariableNameCore(string name, HandlerContext ctx)
        {
            var lowered = name.ToLower();

            if (lowered == "me")
                return "this";

            if (ctx.Locals.Contains(lowered))
                return lowered;

            if (IsGlobal(name, ctx, out var casedName))
                return $"{MovieScriptPrefix(ctx)}{casedName}";

            return $"_global.{name}";
        }

        private static bool IsGlobal(string name, HandlerContext ctx, [NotNullWhen(true)] out string? caseName)
        {
            if (!ctx.Parent.AllGlobals.Contains(name) && !ctx.Globals.Contains(name))
            {
                caseName = null;
                return false;
            }

            if (!ctx.Parent.Parent.AllGlobals.TryGetValue(name, out caseName))
                throw new InvalidOperationException("Global declared but not in all globals list?");

            return true;
        }

        private static string WriteUnaryOperator(
            AstNode.UnaryOperator unaryOperator,
            HandlerContext ctx,
            ExpressionParams? exprParams)
        {
            if (unaryOperator.Type == AstNode.UnaryOperatorType.Not)
            {
                var subParams = new ExpressionParams { WantBool = true };
                var expr = WriteExpression(unaryOperator.Expression, ctx, subParams);

                var sb = new StringBuilder();

                sb.Append(expr);

                if (!subParams.BoolGranted)
                {
                    sb.Insert(0, "LingoGlobal.ToBool(");
                    sb.Append(')');
                }

                sb.Insert(0, '!');

                if (exprParams is not { WantBool: true })
                {
                    sb.Insert(0, '(');
                    sb.Append(" ? 1 : 0)");
                }
                else
                {
                    exprParams.BoolGranted = true;
                }


                return sb.ToString();
            }
            else
            {
                Debug.Assert(unaryOperator.Type == AstNode.UnaryOperatorType.Negate);
                var expr = WriteExpression(unaryOperator.Expression, ctx);

                return $"-{expr}";
            }
        }

        private static string WriteSymbol(AstNode.Symbol node, HandlerContext ctx)
        {
            return $"new LingoSymbol(\"{node.Value}\")";
        }

        private static string WriteString(AstNode.String node, HandlerContext ctx)
        {
            return DoWriteString(node.Value);
        }

        private static string DoWriteString(string str)
        {
            var escaped = str.Replace("\"", "\"\"");
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
            if (node.Property == "char")
                return WriteGlobalCall("charmember_helper", ctx, node.Expression);
            if (node.Property == "line")
                return WriteGlobalCall("linemember_helper", ctx, node.Expression);
            if (node.Property == "length")
                return WriteGlobalCall("lengthmember_helper", ctx, node.Expression);

            var child = WriteExpression(node.Expression, ctx);
            return $"{child}.{WriteSanitizeIdentifier(node.Property.ToLower())}";
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
            var name = WriteSanitizeIdentifier(node.Name.ToLower());
            return $"{child}.{name}({string.Join(',', args)})";
        }

        private static string WriteList(AstNode.List node, HandlerContext ctx)
        {
            if (node.Values.Length == 0)
                return "new LingoList()";

            var args = node.Values.Select(v => WriteExpression(v, ctx));
            return $"new LingoList {{ {string.Join(',', args)} }}";
        }

        private static string WriteGlobalCall(AstNode.GlobalCall node, HandlerContext ctx)
        {
            var args = node.Arguments.Select(a => WriteExpression(a, ctx));
            var lower = node.Name.ToLower();
            if (ctx.Parent.AllHandlers.Contains(lower))
            {
                // Local call
                return $"{lower}({string.Join(',', args)})";
            }

            if (ctx.Parent.Parent.MovieHandlers.Contains(node.Name))
            {
                // Movie script call
                return $"{MovieScriptPrefix(ctx)}{lower}({string.Join(',', args)})";
            }

            return WriteGlobalCall(lower, ctx, args);
        }

        private static string MovieScriptPrefix(HandlerContext ctx)
        {
            return ctx.Parent.IsMovieScript ? "" : "_movieScript.";
        }

        private static string WriteNumber(AstNode.Number node, HandlerContext ctx)
        {
            return $"new LingoNumber({node.Value})";
        }

        private static string WriteConstant(AstNode.Constant node, HandlerContext ctx)
        {
            return $"LingoGlobal.{node.Name.ToUpper()}";
        }

        private static string WriteBinaryOperator(
            AstNode.BinaryOperator node,
            HandlerContext ctx,
            ExpressionParams? param)
        {
            if (param?.WantBool ?? false)
            {
                if (node.Type is AstNode.BinaryOperatorType.Or or AstNode.BinaryOperatorType.And)
                {
                    return WriteBinaryBoolOp(node, ctx, param);
                }

                if (node.Type is >= AstNode.BinaryOperatorType.LessThan and <= AstNode.BinaryOperatorType
                    .GreaterThanOrEqual)
                {
                    return WriteComparisonBoolOp(node, ctx, param);
                }
            }

            // Operators that need to map to special functions.
            var helperOps = node.Type switch
            {
                AstNode.BinaryOperatorType.Contains => "contains",
                AstNode.BinaryOperatorType.Starts => "starts",
                AstNode.BinaryOperatorType.Concat => "concat",
                AstNode.BinaryOperatorType.ConcatSpace => "concat_space",
                AstNode.BinaryOperatorType.LessThan => "op_lt",
                AstNode.BinaryOperatorType.LessThanOrEqual => "op_le",
                AstNode.BinaryOperatorType.NotEqual => "op_ne",
                AstNode.BinaryOperatorType.Equal => "op_eq",
                AstNode.BinaryOperatorType.GreaterThan => "op_gt",
                AstNode.BinaryOperatorType.GreaterThanOrEqual => "op_ge",
                AstNode.BinaryOperatorType.And => "op_and",
                AstNode.BinaryOperatorType.Or => "op_or",
                _ => null
            };

            if (helperOps != null)
                return WriteGlobalCall(helperOps, ctx, node.Left, node.Right);

            var sb = new StringBuilder();

            var op = node.Type switch
            {
                AstNode.BinaryOperatorType.Add => "+",
                AstNode.BinaryOperatorType.Subtract => "-",
                AstNode.BinaryOperatorType.Multiply => "*",
                AstNode.BinaryOperatorType.Divide => "/",
                AstNode.BinaryOperatorType.Mod => "%",
                _ => throw new ArgumentOutOfRangeException()
            };

            sb.Append('(');
            sb.Append(WriteExpression(node.Left, ctx));
            sb.Append(op);
            sb.Append(WriteExpression(node.Right, ctx));
            sb.Append(')');

            return sb.ToString();
        }

        private static string WriteComparisonBoolOp(
            AstNode.BinaryOperator node,
            HandlerContext ctx,
            ExpressionParams expressionParams)
        {
            var exprLeft = WriteExpression(node.Left, ctx);
            var exprRight = WriteExpression(node.Right, ctx);

            expressionParams.BoolGranted = true;

            return node.Type switch
            {
                // Use non-short-circuiting ops.
                AstNode.BinaryOperatorType.LessThan => $"{exprLeft} < {exprRight}",
                AstNode.BinaryOperatorType.LessThanOrEqual => $"{exprLeft} <= {exprRight}",
                AstNode.BinaryOperatorType.GreaterThanOrEqual => $"{exprLeft} >= {exprRight}",
                AstNode.BinaryOperatorType.GreaterThan => $"{exprLeft} > {exprRight}",
                AstNode.BinaryOperatorType.Equal => $"LingoGlobal.op_eq_b({exprLeft}, {exprRight})",
                AstNode.BinaryOperatorType.NotEqual => $"LingoGlobal.op_ne_b({exprLeft}, {exprRight})",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static string WriteBinaryBoolOp(
            AstNode.BinaryOperator node,
            HandlerContext ctx,
            ExpressionParams expressionParams)
        {
            var exprParamLeft = new ExpressionParams { WantBool = true };
            var exprParamRight = new ExpressionParams { WantBool = true };

            var exprLeft = WriteExpression(node.Left, ctx, exprParamLeft);
            var exprRight = WriteExpression(node.Right, ctx, exprParamRight);

            if (!exprParamLeft.BoolGranted)
                exprLeft = $"LingoGlobal.ToBool({exprLeft})";

            if (!exprParamRight.BoolGranted)
                exprRight = $"LingoGlobal.ToBool({exprRight})";

            var op = node.Type switch
            {
                // Use non-short-circuiting ops.
                AstNode.BinaryOperatorType.And => "&",
                AstNode.BinaryOperatorType.Or => "|",
                _ => throw new ArgumentOutOfRangeException()
            };

            expressionParams.BoolGranted = true;

            return $"({exprLeft} {op} {exprRight})";
        }

        private static string WriteGlobalCall(string name, HandlerContext ctx, params AstNode.Base[] args)
        {
            return WriteGlobalCall(name, ctx, args.Select(a => WriteExpression(a, ctx)));
        }

        private static string WriteGlobalCall(string name, HandlerContext ctx, IEnumerable<string> args)
        {
            var method = typeof(LingoGlobal).GetMember(name,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            var isStatic = method.Any(m => m is MethodInfo { IsStatic: true });

            var sb = new StringBuilder();

            sb.Append(isStatic ? "LingoGlobal." : "_global.");
            sb.Append(WriteSanitizeIdentifier(name.ToLower()));
            sb.Append('(');
            sb.Append(string.Join(',', args));
            sb.Append(')');

            return sb.ToString();
        }

        private static readonly HashSet<string> CSharpKeyWords = new HashSet<string>
        {
            "new",
            "string",
            "float"
        };

        private static string WriteSanitizeIdentifier(string identifier)
        {
            return CSharpKeyWords.Contains(identifier) ? $"@{identifier}" : identifier;
        }

        private sealed class GlobalContext
        {
            public GlobalContext(HashSet<string> movieHandlers, string sourcesDest)
            {
                MovieHandlers = movieHandlers;
                SourcesDest = sourcesDest;
            }

            public HashSet<string> AllGlobals { get; } = new(StringComparer.InvariantCultureIgnoreCase);
            public HashSet<string> MovieHandlers { get; }
            public string SourcesDest { get; }
        }

        private sealed class ScriptContext
        {
            public GlobalContext Parent { get; }
            public HashSet<string> AllGlobals { get; }
            public HashSet<string> AllHandlers { get; }
            public bool IsMovieScript { get; }

            public ScriptContext(
                GlobalContext parent,
                HashSet<string> allGlobals,
                HashSet<string> allHandlers,
                bool isMovieScript)
            {
                Parent = parent;
                AllGlobals = allGlobals;
                AllHandlers = allHandlers;
                IsMovieScript = isMovieScript;
            }
        }

        private sealed class HandlerContext
        {
            public HandlerContext(ScriptContext parent, string name, TextWriter writer)
            {
                Parent = parent;
                Name = name;
                Writer = writer;
            }

            public HashSet<string> Globals { get; } = new(StringComparer.InvariantCultureIgnoreCase);
            public HashSet<string> Locals { get; } = new(StringComparer.InvariantCultureIgnoreCase);
            public HashSet<string> DeclaredLocals { get; } = new(StringComparer.InvariantCultureIgnoreCase);
            public ScriptContext Parent { get; }
            public string Name { get; }
            public TextWriter Writer { get; }
        }

        private sealed class ScriptQuirks
        {
            public readonly HashSet<string> BlackListHandlers = new(StringComparer.InvariantCultureIgnoreCase);
            public readonly List<(string, int count)> OverloadParamCounts = new();
        }

        private sealed class ExpressionParams
        {
            public bool WantBool;
            public bool BoolGranted;
        }
    }
}
