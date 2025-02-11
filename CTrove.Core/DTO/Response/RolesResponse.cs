using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class RolesResponse : BaseResponse
    {
        public bool Blinded { get; set; }
        public List<RolesPages>? RolesPages { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
