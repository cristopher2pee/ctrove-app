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
    public interface ISubjectServices
    {
        Task<SubjectResponse?> Save(SubjectRequest req, Guid objId);
        Task<SubjectResponse?> Update(SubjectRequest req, Guid objId);

        Task<bool> Deactivate(DeactivateRequest id, Guid userId);

        Task<SubjectResponse?> GetById(Guid id, Guid objId);
        Task<PagedResult<SubjectResponse>> GetAll(SubjectFilters filters);

        //Subject Phases
        Task<bool> DeactivateSubjectPhase(DeactivateRequest req, Guid objId);
        Task<SubjectPhasesResponse?> UpdateSubjectPhase(SubjectPhasesRequest req, Guid objId);
        Task<SubjectPhasesResponse?> AddSubjectPhase(SubjectPhasesRequest req, Guid objId);

        Task<SubjectPhasesResponse?> GetSubjectPhasebyId(Guid id, Guid objId);
    }
}
