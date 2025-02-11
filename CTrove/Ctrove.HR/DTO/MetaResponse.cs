using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.DTO
{
    public class MetaResponse
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }
}
