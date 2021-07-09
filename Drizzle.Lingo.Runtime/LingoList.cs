using System.Collections.Generic;

namespace Drizzle.Lingo.Runtime
{
    public class LingoList
    {
        public List<dynamic> List { get; }

        public LingoList()
        {
            List = new List<dynamic>();
        }

        public LingoList(IEnumerable<dynamic> items)
        {
            List = new List<dynamic>(items);
        }
    }
}
