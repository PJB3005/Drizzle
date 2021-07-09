using System.Collections.Generic;

namespace Drizzle.Lingo.Runtime
{
    public class LingoParameterList
    {
        public Dictionary<string, dynamic> Dict { get; }

        public LingoParameterList(Dictionary<string, dynamic> dict)
        {
            Dict = dict;
        }

        public LingoParameterList()
        {
            Dict = new Dictionary<string, dynamic>();
        }

        public dynamic this[string index]
        {
            get => Dict[index];
            set => Dict[index] = value;
        }
    }
}
