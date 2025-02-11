using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    public class HrMetaResponse
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }

    public class HrTokenResponse
    {
        public string apiToken { get; set; } = string.Empty;
    }
}
