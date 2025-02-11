﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class SubjectPhases
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
