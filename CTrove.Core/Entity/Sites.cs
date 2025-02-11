using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Sites : BaseEntity
    {
        public Guid StudyCountryId { get; set; }
        public virtual StudyCountry StudyCountry { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SiteStatus SiteStatus { get; set; }
        public Guid ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<SitePhases> SitePhases { get; set; } 
             
    }
}
