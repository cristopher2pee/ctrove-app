using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class AccessListResponse
    {
        public AccessResponse? AccessResponse { get; set; }
        public SitesResponse? SitesResponse { get; set; }
        public StudyCountry? StudyCountry { get; set; }
    }
}
