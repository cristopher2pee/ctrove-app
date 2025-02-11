using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class Audit
    {
        [Key]
        public Guid Id { get; set; }

        public AuditType AuditType { get; set; }

        public Guid PerformedBy { get; set; }

        public DateTime DatePerformed { get; set; }

        public string? FromValue { get; set; }
        public string? ToValue { get; set; }
        public string? AffectedColumns { get; set; }
        public Guid RecordId { get; set; }
        public string Remarks { get; set; }
        public string Table { get; set; }
        public string? Location { get; set; }

    }
}
