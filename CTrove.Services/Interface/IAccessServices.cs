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
    public interface IAccessServices 
    {
        Task<AccessListResponse?> GetById(Guid objectId, Guid id);
        Task<PagedResult<AccessResponse>> GetAll(AccessFilters filters);
        Task<AccessResponse?> Save(Guid objectId,AccessRequest req);
        Task<AccessResponse?> Update(Guid objectId, AccessRequest req);
        Task<bool> Deactivate(Guid objectId, DeactivateRequest req);
       // Task<PagedResult<AccessResponse>> GetAllAccess(AccessFilters filters);

        Task<PagedResult<AccessListResponse>> GetListAccess(AccessFilters filters);

        Task<bool> IsAccessAlreadyExist(AccessFilters filters);
    }
}
