using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class RolesPagesRequest
    {
        public Guid Id { get; set; }
        public Guid RolesId { get; set; }
        public Pages Pages { get; set; }
        public string remarks { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
    }
}
