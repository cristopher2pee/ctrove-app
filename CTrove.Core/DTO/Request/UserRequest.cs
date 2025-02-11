using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class UserRequest : BaseRequest
    {
        public string? Prefix { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string? Middlename { get; set; } = string.Empty;
             

        public string? Suffix { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string? Landline { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Organization { get; set; } = string.Empty;

        // public Guid? RoleId { get; set; }

        public Guid RolesId { get; set; }

    }
}
