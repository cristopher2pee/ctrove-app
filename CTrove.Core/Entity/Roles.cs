using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Roles : BaseEntity
    {
        public bool Blinded { get; set; }

        public virtual ICollection<RolesPages> RolesPages { get; set; }
    }
}
