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

        public dynamic value(string a)
        {
            var parsedExpression = LingoParser.Expression.ParseOrThrow(a);
            var value = ScriptCompiler.Evaluate(parsedExpression, ScriptRuntime);
            return value;
        }
    }
}
