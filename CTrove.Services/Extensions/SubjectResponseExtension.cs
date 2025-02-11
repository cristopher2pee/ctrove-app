using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class SubjectResponseExtension
    {
        public static SubjectResponse ToSubjectResponse(this Subject e)
            => new SubjectResponse
            {
                Id = e.Id,
                Status = e.Status,
                Sex = e.Sex,
                ScreeningNo = e.ScreeningNo,
                Sites = e.Sites,
                SitesId = e.SitesId,
                SubjectPhases = e.SubjectPhases.ToList(),
                SubjectStatus = e.SubjectStatus,
                Ethnicity = e.Ethnicity,
                EthnicityId = e.EthnicityId,
                Race = e.Race,
                RaceId = e.RaceId,
                RandNo = e.RandNo,
                YearOfBirth = e.YearOfBirth,
            };

        public static IEnumerable<SubjectResponse> ToSubjectResponseList(this IEnumerable<Subject> e)
            => e.Select(e => ToSubjectResponse(e)).ToList();

        public static SubjectPhasesResponse ToSubjectPhasesResponse(this SubjectPhases e)
            => new SubjectPhasesResponse
            { 
                Id = e.Id,
                StartDate = e.StartDate,
                Status = e.Status,
                SubjectId = e.SubjectId,
                EndDate = e.EndDate,
                Phase = e.Phase,
                PhaseId = e.PhaseId,
            };
    }
}
