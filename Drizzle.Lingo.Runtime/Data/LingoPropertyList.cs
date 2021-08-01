using System.Collections.Generic;
using System.Dynamic;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    public class LingoPropertyList : DynamicObject
    {
        public Dictionary<dynamic, dynamic?> Dict { get; }

        public int length => Dict.Count;

        public LingoPropertyList(Dictionary<dynamic, dynamic?> dict)
        {
            Dict = dict;
        }

        public LingoPropertyList()
        {
            Dict = new Dictionary<dynamic, dynamic?>();
        }

        public LingoPropertyList(int capacity)
        {
            Dict = new Dictionary<dynamic, dynamic?>(capacity);
        }

        public dynamic? this[dynamic index]
        {
            get => Dict[index];
            set => Dict[index] = value;
        }

        public void addprop(dynamic? key, dynamic? value)
        {
            if (Dict.ContainsKey(key))
                Log.Warning("addprop duplicate key: {Key}", key);

            Dict[key] = value;
        }

        public object? findpos(dynamic? key)
        {
            // findpos is only used as a "does it exist in the list" check so this is fine.
            return Dict.ContainsKey(key) ? new object() : null;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (Dict.TryGetValue(binder.Name, out result) ||
                Dict.TryGetValue(new LingoSymbol(binder.Name), out result)) return true;

            result = null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            Dict[new LingoSymbol(binder.Name)] = value;
            return true;
        }
    }
}
