using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class SitesRequest : BaseRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid StudyCountryId { get; set; }

        public Guid ServiceTypeId { get; set; }

        public SiteStatus SiteStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<SitePhasesRequest>? SitePhases { get; set; }
    }

    public class SitePhasesRequest
    {
        public Guid Id { get; set; }
        public Guid SitesId { get; set; }
        public Guid PhaseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public string? Remarks { get; set; }
        public string? Locations { get; set; }
    }
}
