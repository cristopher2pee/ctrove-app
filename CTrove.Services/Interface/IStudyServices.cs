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
    public interface IStudyServices
    {
        Task<StudyResponse?> GetById(Guid objId, Guid id);
        Task<PagedResult<StudyResponse>> GetAll(BaseFilters filters);
        Task<StudyResponse?> Save(Guid objId, StudyRequest req);
        Task<StudyResponse?> Update(Guid objId, StudyRequest req);
        Task<bool> Deactivate(Guid objId, DeactivateRequest id);

        Task<bool> IsExist(string param);

    }
}
