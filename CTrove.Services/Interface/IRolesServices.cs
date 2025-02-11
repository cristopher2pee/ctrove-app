using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface IRolesServices
    {
        Task<Roles?> Save(RolesRequest entity, Guid userId);
        Task<Roles?> Update(RolesRequest entity, Guid userId);
        Task<bool> Deactivate(DeactivateRequest id, Guid userId);
        Task<Roles?> GetById(Guid guid, Guid userId);
        Task<PagedResult<RolesResponse>> GetAll(RolesFilters filters);
        Task<List<Roles>> GetList();

        Task<bool> IsExist(string param);
    }
}
