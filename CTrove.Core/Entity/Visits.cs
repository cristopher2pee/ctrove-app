using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Visits : BaseEntity
    {
        public int VisitType { get; set; }
        public int TargetDays { get; set; }
        public int TimeWindow { get; set; }

        public Guid? VisitId { get; set; } 
        public bool isRequired { get; set; }


    }
}
