using CTrove.Core.Interface;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Services
{
    public class PermissionService : IPermissionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> AccessUserRoles(Guid guid)
        {
           string userRoles = string.Empty;
           var entity = await _unitOfWork._User.GetDbSet()
                .Where(f=>f.ObjectId == guid)
                .Include(f=>f.Roles)
                .FirstOrDefaultAsync();

           if (entity is null) return userRoles;
           return userRoles = entity.Roles.Code;
          
        }
    }
}
