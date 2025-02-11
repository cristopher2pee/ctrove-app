using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Contributor
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public PrefixType Prefix { get; set; }
        //public string? Prefix { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public GradeType Grade { get; set; }
       // public string? Grade { get; set; } = string.Empty;
        public string? PrimaryJobTitle { get; set; } = string.Empty;
        public string? SecondaryJobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public bool PublicData { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public bool Active { get; set; }
        public bool Consent { get; set; }
        public DateTime? DateOfConsent { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<ContributorStudy> ContributorStudy { get; set; }
    }
}
