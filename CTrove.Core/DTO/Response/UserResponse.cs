using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class UserResponse : BaseResponse
    {
        public Guid? ObjectId { get; set; }
        public string Prefix { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string Suffix { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Landline { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Organization { get; set; } = string.Empty;

        // public Guid? RoleId { get; set; }

        public Guid? RolesId { get; set; }
        public Roles? Roles { get; set; }
    }

    public class UserAccessResponse
    {
        //public UserResponse? UserResponse { get; set; }
        public User? UserResponse { get; set; }
        public List<Access>? AccessListResponse { get; set; } = new List<Access> { };
        public List<Sites>? SitesListResponse { get; set; }  = new List<Sites> { };
        public List<StudyCountry>? StudyCountryListResponse { get; set; } = new List<StudyCountry> { };
       // public SiteRes
    }
}
