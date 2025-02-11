using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Filters
{
    internal class Filters
    {
    }

    public class AuditTrailFilters : BaseFilters
    {
        public string Table { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public Guid RecordId { get; set; }
        public AuditType? AuditType { get; set; }

    }

    public class RolesFilters : BaseFilters
    {
        public bool isBlinded { get; set; } = true;
    }

    public class AccessFilters : BaseFilters
    {
        public int Right { get; set; }
        public Guid? accessLevelId { get; set; }
        public Guid? userId { get; set; }
    }

    public class UserFilters : BaseFilters
    {
        public List<Guid>? RolesId { get; set; }
    }

    public class SitesFilters : BaseFilters
    {
        public List<Guid>? StudyCountryId { get; set; }
        public List<Guid>? ServiceTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SiteStatus>? SiteStatusId { get; set; }
    }

    public class VisitsFilters : BaseFilters
    {
        public int VisitType { get; set; }
    }

    public class RolesPagesFilters : BaseFilters
    {
        public Guid RolesId { get; set; }
    }

    public class SubjectFilters : BaseFilters
    {
        public List<Guid>? EthnicityIds { get; set; }
        public List<Guid>? RaceIds { get; set; }
        public List<Guid>? SitesIds { get; set; }
        public List<SubjectStatus>? SubjectStatus { get; set; }
        public List<Guid>? StudyCountryIds { get; set; }
    }

    public class OrganizationFilter : BaseFilters
    {
        public List<Guid>? ListCountryId { get; set; }
        //public List<Guid>? ListContactTypeId { get; set; }
        //public List<Guid>? ListVendorTypeId { get; set; }
        public List<ParentType>? parentTypes { get; set; }
    }

    public class ContributorFiltes : BaseFilters
    {
        public List<Guid>? ListCountryId { get; set; }
        public List<Guid>? ListOrganizationId { get; set; }
    }

    public class ContributorStudyFilters : BaseFilters
    {
        public List<Guid>? ListStudyIds { get; set; }
        public List<Guid>? ListContributorIds { get; set; }
    }

}
