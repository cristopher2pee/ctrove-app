using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    public class HrContributorStudyRequest : HrBaseRequest
    {
        public Guid Id { get; set; }
        public Guid StudyId { get; set; }
        public string StudyName { get; set; } = string.Empty;
        public Guid ContributorId { get; set; }

        public Guid? SponsorId { get; set; }
        public string? Role { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
    }

    public class HrContributorStudyResponse
    {
        public Guid Id { get; set; }
        public Guid StudyId { get; set; }
        public string StudyName { get; set; } = string.Empty;
        public Guid ContributorId { get; set; }
        public Contributor? Contributor { get; set; }

        public Guid? SponsorId { get; set; }
        public string? Role { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
    }
}
