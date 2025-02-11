using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class AccessRequest : BaseRequest
    {
        public Guid AccessLevelId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; }
        public int Rights { get; set; }
        
    }
}
