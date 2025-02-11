using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    public class HrBaseRequest
    {
        public Guid UserObjectId { get; set; }
        public string? Location { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
    }
}
