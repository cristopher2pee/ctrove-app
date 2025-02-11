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
    public interface IStudyCountryServices
    {
        Task<StudyCountry?> Save(DefaultRequest entity, Guid userId);
        Task<StudyCountry?> Update(DefaultRequest entity, Guid userId);
        Task<bool> Delete(DeactivateRequest id, Guid userId);
        Task<StudyCountry?> GetById(Guid guid, Guid userId);
        Task<PagedResult<StudyCountry>> GetAll(BaseFilters filters);

        Task<bool> IsExist(string param);
    }
}
