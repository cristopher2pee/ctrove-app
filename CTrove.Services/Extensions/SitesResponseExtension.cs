using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class SitesResponseExtension
    {
        public static SitesResponse ToSitesResponse(this Sites entity)
        {
            return new SitesResponse
            {
                Id = entity.Id,
                SiteStatus = entity.SiteStatus,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                StudyCountry = entity.StudyCountry,
                StudyCountryId = entity.StudyCountryId,
                ServiceType = entity.ServiceType,
                ServiceTypeId = entity.ServiceTypeId,
                Code = entity.Code,
                Name = entity.Name,
                SitePhases = entity.SitePhases is not null ? entity.SitePhases
                    .Select(e => new SitePhasesResponse
                    {
                        Id = e.Id,
                        EndDate = e.EndDate,
                        SitesId = e.SitesId,
                        StartDate = e.StartDate,
                        Status = e.Status,
                        PhaseId = e.PhaseId,
                        Phase = e.Phase
                    }).ToList() : null
                
            };

        }

        public static IEnumerable<SitesResponse> ToSitesResponsePagedList(this IEnumerable<Sites> entities)
            => entities.Select(e => e.ToSitesResponse()).ToList();

        public static List<SitesResponse> ToSiteResponseList(this List<Sites> entities)
            => entities.Select(f => f.ToSitesResponse()).ToList();


    }
}  
