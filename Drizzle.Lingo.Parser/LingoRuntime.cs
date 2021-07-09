using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace Drizzle.Lingo.Parser
{
    public sealed class LingoRuntime
    {
        private readonly Dictionary<string, GetMemberBinder> _getMemberBinders = new();
        private readonly Dictionary<ExpressionType, BinaryOperationBinder> _binaryOperationBinders = new();

        public GetMemberBinder GetGetMemberBinder(string memberName)
        {
            if (!_getMemberBinders.TryGetValue(memberName, out var binder))
                _getMemberBinders[memberName] = binder = new LingoGetMemberBinder(memberName);

            return binder;
        }

        public BinaryOperationBinder GetBinaryOperationBinder(ExpressionType type)
        {
            if (!_binaryOperationBinders.TryGetValue(type, out var binder))
                _binaryOperationBinders[type] = binder = new LingoBinaryOperationBinder(type);

            return binder;
        }
    }
}
