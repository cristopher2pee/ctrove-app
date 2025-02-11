using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
       // public virtual Country Country { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public ParentType Parent { get; set; }
        //public string Parent { get; set; } = string.Empty;
        public Guid ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public Guid VendorTypeId { get; set; }
        public virtual VendorType VendorType { get; set; }
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
      //  public virtual Contributor PrimaryContributorContact { get; set; }
        public Guid? SecondaryContactId { get; set; }
       // public virtual Contributor SecondaryContributorContact { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
