using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class AccountsRequest
    {
        public string email { get; set; }
        public Guid rolesId { get; set; }
    }
}
