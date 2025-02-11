﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Study : BaseEntity
    {
        public Guid? TherapeuticAreaId { get; set; }
        public virtual TherapeuticArea TherapeuticArea { get; set; }

        public string StudyType { get; set; } = string.Empty;

        public Guid? ClassificationId { get; set; }
        public virtual Classification Classification { get; set; }
        public string BillingCode { get; set; } = string.Empty;
        public string Sponsor { get; set; } = string.Empty;
        public string? ApiKeyToken { get; set;} = string.Empty;
    }
}
