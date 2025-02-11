using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface IVisitsServices
    {
        Task<PagedResult<VisitsResponse>> GetList(VisitsFilters filter);
        
        Task<VisitsResponse> GetById(Guid objId, Guid id);

        Task<VisitsResponse> Save(Guid objId, VisitsRequest req);

        Task<VisitsResponse> Update(Guid objId, VisitsRequest req);

        Task<bool> Deactivate(Guid objId, DeactivateRequest req);

        Task<bool> IsExist(string param);
    }
}
