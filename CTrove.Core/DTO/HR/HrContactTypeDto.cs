using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    public class HrContactTypeDto
    {

    }

    public class HrContactTypeRequest : HrBaseRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

    public class HrContactTypeResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
