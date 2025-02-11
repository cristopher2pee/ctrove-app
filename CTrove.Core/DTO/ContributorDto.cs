using CTrove.Core.DTO.HR;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Enum;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CTrove.Core.DTO
{
    internal class ContributorDto
    {
    }

    public class ContributorRequest : BaseRequest
    {
        //public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Guid CountryId { get; set; }
        public string Email { get; set; } = string.Empty;
        public PrefixType Prefix { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public GradeType Grade { get; set; }
        public string? PrimaryJobTitle { get; set; } = string.Empty;
        public string? SecondaryJobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public bool PublicData { get; set; }
        public Guid OrganizationId { get; set; }
        public string City { get; set; } = string.Empty;
        public bool Consent { get; set; }
        public bool Active { get; set; }
        // public bool Status { get; set; }
        public DateTime? DateOfConsent { get; set; }
    }

    public class ContributorResponse
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public string Email { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public PrefixType Prefix { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public GradeType Grade { get; set; }
        public string? PrimaryJobTitle { get; set; } = string.Empty;
        public string? SecondaryJobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public bool PublicData { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }
        public bool Consent { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
        public DateTime? DateOfConsent { get; set; }
        public List<ContributorStudy>? ContributorStudy { get; set; }
    }

    public static class ContributorResponseExtension
    {
        public static ContributorResponse ToContributorResponse(this Contributor result)
            => new ContributorResponse
            {
                Id = result.Id,
                ObjectId = result.ObjectId,
                CountryId = result.CountryId,
                Country = result.Country,
                Email = result.Email,
                Prefix = result.Prefix,
                Firstname = result.Firstname,
                Lastname = result.Lastname,
                Grade = result.Grade,
                PrimaryJobTitle = result.PrimaryJobTitle,
                SecondaryJobTitle = result.SecondaryJobTitle,
                Phone = result.Phone,
                Mobile = result.Mobile,
                PublicData = result.PublicData,
                OrganizationId = result.OrganizationId,
                Organization = result.Organization,
                City = result.City,
                Consent = result.Consent,
                Status = result.Status,
                DateOfConsent = result.DateOfConsent,
                ContributorStudy = result.ContributorStudy != null ? result.ContributorStudy.ToList() : null
            };

        public static IEnumerable<ContributorResponse> ToContributorResponseList(this IEnumerable<Contributor> entities)
            => entities.Select(e => e.ToContributorResponse()).ToList();

        public static HrContributorRequest ConvertToHrContributor(this Contributor entity, HrBaseRequest req)
            => new HrContributorRequest
            {
                Id = entity.Id,
                ObjectId = entity.ObjectId,
                CountryId = entity.CountryId,
                Email = entity.Email,
                Prefix = entity.Prefix,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Grade = entity.Grade,
                PrimaryJobTitle = entity.PrimaryJobTitle,
                SecondaryJobTitle = entity.SecondaryJobTitle,
                Phone = entity.Phone,
                MobilePhone = entity.Mobile,
                PublicData = entity.PublicData,
                OrganizationId = entity.OrganizationId,
                City = entity.City,
                Consent = entity.Consent,
                Status = entity.Status,
                Location = req.Location,
                Remarks = req.Remarks,
                UserObjectId = req.UserObjectId,
                DateOfConsent = entity.DateOfConsent,
            };
    }
}
    