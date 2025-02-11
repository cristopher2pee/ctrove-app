using CTrove.Core.DTO.HR;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO
{
    internal class ContributorStudyDto
    {
    }

    public class ContributorStudyRequest
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
        public string? Remarks { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
    }

    public class ContributorStudyResponse
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

    public static class ContributorStudyExtension
    {
        public static ContributorStudyResponse ToContributorStudyResponse(this ContributorStudy entity)
            => new ContributorStudyResponse
            {
                Id = entity.Id,
                StudyId = entity.StudyId,
                StudyName = entity.StudyName,
                ContributorId = entity.ContributorId,
                Role = entity.Role,
                SponsorId = entity.SponsorId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                
            };

        public static IEnumerable<ContributorStudyResponse> ToContributorStudyResponseList(this IEnumerable<ContributorStudy> entities)
            => entities.Select(e => e.ToContributorStudyResponse()).ToList();

        public static HrContributorStudyRequest ConvertToHrContributorStudy(this ContributorStudy entity, HrBaseRequest req)
            => new HrContributorStudyRequest
            {
                Id = entity.Id,
                StudyId = entity.StudyId,
                StudyName = entity.StudyName,
                ContributorId = entity.ContributorId,
                StartDate = entity.StartDate,
                EndDate= entity.EndDate,
                SponsorId= entity.SponsorId,
                Role = entity.Role,
                Status = entity.Status,
                Location = req.Location,
                Remarks = req.Remarks,
                UserObjectId = req.UserObjectId
            };
    }

    

    
}
