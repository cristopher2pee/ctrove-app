using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Model
{
    public class HrOrganization
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        public Guid ContactTypeId { get; set; }
        public Guid VendorTypeId { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FaxNumber { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public Guid PrimaryContact { get; set; }
        public Guid SecondaryContact { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string MyTime { get; set; } = string.Empty;
        public Guid ParentId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
