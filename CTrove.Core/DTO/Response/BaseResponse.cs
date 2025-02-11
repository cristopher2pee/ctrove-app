using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
