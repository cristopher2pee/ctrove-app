using CTrove.Core.Entity;
using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    public class HrContributorRequest : HrBaseRequest
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Guid CountryId { get; set; }
        //public Country? Country { get; set; }
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public PrefixType Prefix { get; set; }
        public string? Firstname { get; set; } = string.Empty;
        public string? Lastname { get; set; } = string.Empty;
        public GradeType Grade { get; set; }
        public string? PrimaryJobTitle { get; set; } = string.Empty;
        public string? SecondaryJobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? MobilePhone { get; set; } = string.Empty;
        public bool PublicData { get; set; }
        public Guid OrganizationId { get; set; }
      //  public Organization? Organization { get; set; }

        public bool Consent { get; set; }
        public DateTime? DateOfConsent { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        //public List<ContributorStudy>? ContributorStudies { get; set; }
    }

    public class HrContributorPageResponse
    {
        List<HrContributorResponse> data { get; set; }
        HrMetaResponse meta { get; set; }
    }

    public class HrContributorResponse
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public string Email { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public PrefixType Prefix { get; set; }
        public string? Firstname { get; set; } = string.Empty;
        public string? Lastname { get; set; } = string.Empty;
        public GradeType Grade { get; set; }
        public string? PrimaryJobTitle { get; set; } = string.Empty;
        public string? SecondaryJobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? MobilePhone { get; set; } = string.Empty;
        public bool PublicData { get; set; }
        public Guid OrganizationId { get; set; }
        public HrOrganizationResponse? Organization { get; set; }

        public bool Consent { get; set; }
        public bool Status { get; set; }
        public DateTime? DateOfConsent { get; set; }
        public bool Active { get; set; }
        public List<ContributorStudy>? ContributorStudies { get; set; }
    }
}
