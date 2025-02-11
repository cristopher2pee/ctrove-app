using CTrove.Core.DTO.HR;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO
{
    internal class OrganizationDto
    {
    }

    public class OrganizationRequest : BaseRequest
    {
        //public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public Enum.ParentType Parent { get; set; }
        public Guid ContactTypeId { get; set; }
        public Guid VendorTypeId { get; set; }
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string? AddressLine3 { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? FaxNumber { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public Guid? PrimaryContactId { get; set; }
        public Guid? SecondaryContactId { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        
    }

    public class OrganizationResponse
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public Enum.ParentType Parent { get; set; }
        public Guid ContactTypeId { get; set; }
        public virtual ContactType? ContactType { get; set; }
        public Guid VendorTypeId { get; set; }
        public virtual VendorType? VendorType { get; set; }
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string? AddressLine3 { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? FaxNumber { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public Guid? PrimaryContactId { get; set; }
        public virtual Contributor? PrimaryContributorContact { get; set; }
        public Guid? SecondaryContactId { get; set; }
        public virtual Contributor? SecondaryContributorContact { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

    public static class OrganizationResponseExtension
    {
        public static HrOrganizationRequest ConvertToHrOrganizationRequest(this Organization item, HrBaseRequest req)
            => new HrOrganizationRequest
            {
                Id = item.Id,
                CountryId = item.CountryId,
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                VendorTypeId = item.VendorTypeId,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                SecondaryContactId = item.SecondaryContactId,
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
                UserObjectId = req.UserObjectId,
                Remarks = req.Remarks,
                Location = req.Location
            };

        public static Organization ConvertOrganizationRequestToEntity(this OrganizationRequest req, bool isNew)
            => new Organization
            {
                Id = isNew ? Guid.NewGuid() : req.Id,
                CountryId = req.CountryId,
                CompanyName = req.CompanyName,
                Parent = req.Parent,
                ContactTypeId = req.ContactTypeId,
                VendorTypeId = req.VendorTypeId,
                AddressLine1 = req.AddressLine1,
                AddressLine2 = req.AddressLine2,
                AddressLine3 = req.AddressLine3,
                ZipCode = req.ZipCode,
                City = req.City,
                State = req.State,
                PhoneNumber = req.PhoneNumber,
                FaxNumber = req.FaxNumber,
                Website = req.Website,
                PrimaryContactId = req.PrimaryContactId,
                SecondaryContactId = req.SecondaryContactId,
                Email = req.Email,
                Notes = req.Notes,
                Status = req.Status,
            };
    }
}
