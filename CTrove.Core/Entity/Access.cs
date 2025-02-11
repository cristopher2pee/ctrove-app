using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Access 
    {
        public Guid Id { get; set; }
        public Guid AccessLevelId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int Rights { get; set; }
        public bool Status { get; set; }


    }
}
