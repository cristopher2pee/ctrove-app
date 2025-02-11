using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class StudyResponseExtension
    {
        public static StudyResponse ToStudyResponse(this Study entity)
        {
            return new StudyResponse
            {
                Id = entity.Id,
                Sponsor = entity.Sponsor,
                Status = entity.Status,
                BillingCode = entity.BillingCode,
                TherapeuticAreaId = entity.TherapeuticAreaId,
                TherapeuticArea = entity.TherapeuticArea != null ? entity.TherapeuticArea : null,
                ClassificationId = entity.ClassificationId,
                Classification = entity.Classification != null ? entity.Classification : null,
                StudyType = entity.StudyType,
                Code = entity.Code,
                Name = entity.Name,
                ApiKeyToken = entity.ApiKeyToken,
            };
        }

        public static List<StudyResponse> ToStudyResponseList(this List<Study> entities)
        {
            var res = entities.Select(f => f.ToStudyResponse()).ToList();
            return res;
        }
    }
}
