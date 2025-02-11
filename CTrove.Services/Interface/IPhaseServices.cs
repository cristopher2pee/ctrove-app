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
    public interface IPhaseServices
    {

        Task<Phase?> Save(PhaseRequest entity, Guid userId);
        Task<Phase?> Update(PhaseRequest entity, Guid userId);
        Task<bool> Deactivate(DeactivateRequest req, Guid userId);
        Task<Phase?> GetById(Guid guid, Guid userId);
        Task<PagedResult<Phase>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
