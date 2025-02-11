using CTrove.Core.Entity;
using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class SitesResponse : BaseResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid? StudyCountryId { get; set; }

        public virtual StudyCountry StudyCountry { get; set; }

        public Guid? ServiceTypeId { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        public SiteStatus SiteStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<SitePhasesResponse>? SitePhases { get; set; }

    }

    public record struct SitesResponseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Guid StudyCountryId { get; set; }
        public StudyCountry StudyCountry { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

        public SiteStatus SiteStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SitePhasesResponseDto> SitePhases { get; set; }

        public bool Status { get; set; }
    }

    public record struct SitePhasesResponseDto
    {
        public Guid Id { get; set; }
        public Guid SitesId { get; set; }
        public Guid PhaseId { get; set; }
        public Phase Phase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }

    public class SitePhasesResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public Guid SitesId { get; set; }
        public Guid PhaseId { get; set; }
        public Phase Phase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }

}
