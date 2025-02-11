using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.HR
{
    internal class HrOrganizationDto
    {
    }

    public class HrOrganizationRequest : HrBaseRequest
    {
        public Guid Id { get; set; }
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
        public bool Status { get; set; }
    }

    public class HrOrganizationResponse
    {
        public Guid Id { get; set; }
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
        public bool Status { get; set; }
    }
}
