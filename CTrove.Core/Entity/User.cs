using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class User
    {

        public Guid Id { get; set; }
        public Guid? ObjectId { get; set; }

        public string? Prefix { get; set; }

        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string? Middlename { get; set; } 

        public string? Suffix { get; set; }


        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string? Landline { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Organization { get; set; } = string.Empty;

        // public Guid? RoleId { get; set; }

        public Guid RolesId { get; set; }
        public virtual Roles Roles { get; set; }

        public bool Status { get; set; }

        //  public virtual ICollection<Access> Access { get; set; }
    }
}
