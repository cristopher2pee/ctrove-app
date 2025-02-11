using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface IPermissionServices
    {
         Task<string> AccessUserRoles(Guid guid);
    }
}
