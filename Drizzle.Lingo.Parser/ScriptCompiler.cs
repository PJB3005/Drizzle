using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Drizzle.Lingo.Parser.Ast;
using Drizzle.Lingo.Runtime;

namespace Drizzle.Lingo.Parser
{
    public static class ScriptCompiler
    {
        public static Delegate CompileHandler(AstNode.Handler handler, LingoRuntime runtime)
        {
            var scope = new CompileScope(runtime);

            var paramList = new List<ParameterExpression>();
            foreach (var paramName in handler.Parameters)
            {
                var paramExpr = Expression.Parameter(typeof(object), paramName);
                paramList.Add(paramExpr);
                scope.Locals.Add(paramName, paramExpr);
            }

            scope.RetLabel = Expression.Label(typeof(object), "ret");

            var topBlockExpressions = new List<Expression>();
            foreach (var statement in handler.Body.Statements)
            {
                var expr = CompileNode(statement, scope);

                topBlockExpressions.Add(expr);
            }

            topBlockExpressions.Add(Expression.Label(scope.RetLabel, Expression.Constant(null)));

            var block = Expression.Block(scope.DeclaredLocals, topBlockExpressions);
            var delegateType = Expression.GetFuncType(Enumerable.Repeat(typeof(object), paramList.Count + 1).ToArray());
            var lambda = Expression.Lambda(delegateType, block, handler.Name, paramList);

            return lambda.Compile();
        }

        private static Expression CompileNode(AstNode.Base node, CompileScope scope)
        {
            return node switch
            {
                AstNode.Assignment ass => CompileAssigment(ass, scope),
                AstNode.Return ret => CompileReturn(ret, scope),
                AstNode.VariableName varName => CompileVariableName(varName, scope),
                AstNode.GlobalCall globalCall => CompileGlobalCall(globalCall, scope),
                AstNode.BinaryOperator binOp => CompileBinaryOperator(binOp, scope),
                AstNode.MemberProp memProp => CompileMemberProp(memProp, scope),
                _ => throw new NotImplementedException()
            };
        }

        private static Expression CompileMemberProp(AstNode.MemberProp node, CompileScope scope)
        {
            return Expression.Dynamic(
                scope.Runtime
                    .GetGetMemberBinder(node.Property),
                typeof(object),
                CompileNode(node.Expression, scope)
            );
        }

        private static Expression CompileBinaryOperator(AstNode.BinaryOperator node, CompileScope scope)
        {
            var mappedBinOp = node.Type switch
            {
                AstNode.BinaryOperatorType.Subtract => ExpressionType.Subtract,
                AstNode.BinaryOperatorType.Add => ExpressionType.Add,
                AstNode.BinaryOperatorType.Multiply => ExpressionType.Multiply,
            };

            return Expression.Dynamic(
                scope.Runtime.GetBinaryOperationBinder(mappedBinOp),
                typeof(object),
                CompileNode(node.Left, scope),
                CompileNode(node.Right, scope)
            );
        }

        private static Expression CompileGlobalCall(AstNode.GlobalCall node, CompileScope scope)
        {
            switch (node.Name.ToLower())
            {
                /*case "abs":
                {
                    var val = CompileNode(node.Arguments[0], scope);

                    var method = typeof(LingoGlobal).GetMethod(nameof(LingoGlobal.Abs))!;
                    return Expression.Call(null, method, val);
                    break;
                }
                case "sqrt":
                {
                    var val = CompileNode(node.Arguments[0], scope);

                    var method = typeof(LingoGlobal).GetMethod(nameof(LingoGlobal.Sqrt))!;
                    return Expression.Call(null, method, val);
                    break;
                }*/
                default:
                    throw new NotSupportedException();
            }
        }

        private static Expression CompileVariableName(AstNode.VariableName node, CompileScope scope)
        {
            var localName = node.Name;
            if (!scope.Locals.TryGetValue(localName, out var local))
            {
                local = Expression.Parameter(typeof(object), localName);
                scope.Locals.Add(localName, local);
                scope.DeclaredLocals.Add(local);
            }

            return local;
        }

        private static Expression CompileReturn(AstNode.Return ret, CompileScope scope)
        {
            var value = ret.Value != null ? CompileNode(ret.Value, scope) : Expression.Constant(null);

            return Expression.Goto(scope.RetLabel, value);
        }

        private static Expression CompileAssigment(AstNode.Assignment node, CompileScope scope)
        {
            var local = CompileVariableName((AstNode.VariableName) node.Assigned, scope);
            var value = CompileNode(node.Value, scope);

            return Expression.Assign(local, value);
        }

        /*
        private static Expression CompileStatementBlock(AstNode.StatementBlock block)
        {

        }
        */

        private sealed class CompileScope
        {
            public readonly LingoRuntime Runtime;

            public readonly Dictionary<string, ParameterExpression> Locals = new();
            public readonly List<ParameterExpression> DeclaredLocals = new();
            public LabelTarget RetLabel = default!;

            public CompileScope(LingoRuntime runtime)
            {
                Runtime = runtime;
            }
        }
    }
}
