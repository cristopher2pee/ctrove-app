using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class DeactivateRequest
    {
        public Guid Id { get; set; }
        public string? Remarks { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
    }
}
