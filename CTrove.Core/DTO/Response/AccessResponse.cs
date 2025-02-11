using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class AccessResponse : BaseResponse
    {
        public Guid AccessLevelId { get; set; } 
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int Rights { get; set; }
    }
}
