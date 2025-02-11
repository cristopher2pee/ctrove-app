using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Common
{
    public class PagedMeta
    {
        public int Total { get; set; } = 0;
        public int Limit { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int LastPage { get; set; }
    }
}
