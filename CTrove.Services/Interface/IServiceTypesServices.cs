using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;

namespace CTrove.Services.Interface
{
    public interface IServiceTypesServices
    {
        Task<ServiceType?> Save(DefaultRequest serviceType, Guid userId);
        Task<ServiceType?> Update(DefaultRequest serviceType, Guid userId);
        Task<bool> Deactivate(DeactivateRequest id, Guid userId);
        Task<IEnumerable<ServiceType>> GetAll();
        Task<ServiceType?> GetById(Guid guid, Guid userId);

        Task<PagedResult<ServiceType>> GetAllServiceType(BaseFilters filters);

        Task<bool> IsExist(string param);


    }
}
