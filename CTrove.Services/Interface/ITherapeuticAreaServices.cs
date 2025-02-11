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
    public interface ITherapeuticAreaServices
    {
        Task<TherapeuticArea?> Save(DefaultRequest entity, Guid userId);
        Task<TherapeuticArea?> Update(DefaultRequest entity, Guid userId);
        Task<bool> Delete(DeactivateRequest id, Guid userId);
        Task<TherapeuticArea?> GetById(Guid guid, Guid userId);
        Task<PagedResult<TherapeuticArea>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
