using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface IRolesPagesServices
    {
        Task<RolesPages?> Save(RolesPagesRequest req, Guid objId);
        Task<RolesPages?> Update(RolesPagesRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);
        Task<RolesPages?> GetById(Guid id, Guid objId);

        Task<PagedResult<RolesPages>> GetAll(RolesPagesFilters req);
    }
}
