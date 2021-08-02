using System;
using System.Diagnostics.CodeAnalysis;
using Drizzle.Lingo.Runtime.Parser;
using Drizzle.Lingo.Runtime.Scripting;
using Pidgin;

namespace Drizzle.Lingo.Runtime
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed partial class LingoGlobal
    {
        public LingoScriptRuntime ScriptRuntime { get; private set; } = default!;

        public dynamic? value(string a)
        {
            var trimmed = a.AsSpan().Trim();
            if (trimmed.IsEmpty)
                return 0; // value() returns zero on empty string.

            var parsedExpression = LingoParser.Expression.ParseOrThrow(trimmed);
            return Interpreter.Evaluate(parsedExpression, LingoRuntime);
        }
    }
}
