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
    public interface IRaceServices
    {
        Task<Race?> Save(RaceRequest entity, Guid objId);

        Task<Race?> Update(RaceRequest entity, Guid objId);

        Task<bool> Deactivate(DeactivateRequest req, Guid objId);

        Task<Race?> GetbyId(Guid id, Guid objId);
        Task<PagedResult<Race>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
