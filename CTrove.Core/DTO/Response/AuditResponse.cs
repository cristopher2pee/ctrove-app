using CTrove.Core.Entity;
using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Response
{
    public class AuditResponse
    {
        public Guid Id { get; set; }
        public Guid PerformedBy { get; set; }
        public UserResponse? UserResponse { get; set; }
        public DateTime DatePeformed { get; set; }
        public string FromValue { get; set; } = string.Empty;
        public string ToValue { get; set; } = string.Empty;
        public string AffectedColumn { get; set; } = string.Empty;
        public Guid RecordId { get; set; }
        public string Table { get; set; } = string.Empty;
        public AuditType AuditType { get; set; }

        public string Remarks { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class AuditResponseWithUser
    {
        public Audit? Audit { get; set; }
        public User? User { get; set; }
    }

    public class AuditRetrievedRequest
    {
        public Object obj { get; set; }
        public Guid recordId { get; set; }
        public string Table { get; set; }
        public AuditType AuditType { get; set; }
        public Guid PerformedBy { get; set; }
    }
}
