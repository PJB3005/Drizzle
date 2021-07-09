using System.Collections.Generic;

namespace Drizzle.Lingo.Runtime
{
    public class LingoPropertyList
    {
        public Dictionary<dynamic, dynamic> Dict { get; }

        public LingoPropertyList(Dictionary<dynamic, dynamic> dict)
        {
            Dict = dict;
        }

        public LingoPropertyList()
        {
            Dict = new Dictionary<dynamic, dynamic>();
        }

        public dynamic this[dynamic index]
        {
            get => Dict[index];
            set => Dict[index] = value;
        }
    }
}
