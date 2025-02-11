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
    public interface IEthnicityServices
    {
        Task<Ethnicity?> Save(EthnicityRequest entity, Guid objId);

        Task<Ethnicity?> Update(EthnicityRequest entity, Guid objId);

        Task<bool> Deactivate(DeactivateRequest req, Guid objId);

        Task<Ethnicity?> GetbyId(Guid id, Guid objId);
        Task<PagedResult<Ethnicity>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
