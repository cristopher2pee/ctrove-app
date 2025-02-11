using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Common
{
    public class QueryParamBuilder : List<KeyValuePair<string, object>>
    {
        public void Add(string key, object value)
        {
            var element = new KeyValuePair<string, object>(key, value);
            this.Add(element);
        }
    }
}
