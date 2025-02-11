using CTrove.Core.Entity;
using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class SubjectResponse
    {
        public Guid Id { get; set; }
        public string ScreeningNo { get; set; } = string.Empty;
        public string RandNo { get; set; } = string.Empty;
        public Guid SitesId { get; set; }
        public virtual Sites Sites { get; set; }
        public int YearOfBirth { get; set; }
        public int Sex { get; set; }
        public Guid EthnicityId { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public Guid RaceId { get; set; }
        public virtual Race Race { get; set; }
        public SubjectStatus SubjectStatus { get; set; }
        public bool Status { get; set; }
        public List<SubjectPhases> SubjectPhases { get; set; }
    }

    public class SubjectPhasesResponse
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Guid PhaseId { get; set; }
        public virtual Phase Phase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
