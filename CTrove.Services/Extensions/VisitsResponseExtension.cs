using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class VisitsResponseExtension
    {
        public static VisitsResponse ToVisitsResponse(this Visits entity)
        {
            return new VisitsResponse
            {
                Id = entity.Id,
                Status = entity.Status,
                Code = entity.Code,
                isRequired = entity.isRequired,
                Name = entity.Name,
                TargetDays = entity.TargetDays,
                TimeWindow = entity.TimeWindow,
                VisitId = entity.VisitId,
                VisitType = entity.VisitType,
            };
        }

        public static IEnumerable<VisitsResponse> ToVisitsResponseList(this IEnumerable<Visits> entities)
            => entities.Select(e => e.ToVisitsResponse()).ToList();
    }
}
