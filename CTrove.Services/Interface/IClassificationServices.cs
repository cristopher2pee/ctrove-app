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
    public interface IClassificationServices
    {

        Task<Classification?> Save(DefaultRequest entity, Guid userId);
        Task<Classification?> Update(DefaultRequest entity, Guid userId);
        Task<bool> Deactivate(DeactivateRequest req, Guid userId);
        Task<Classification?> GetById(Guid guid, Guid objId);
        Task<PagedResult<Classification>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
