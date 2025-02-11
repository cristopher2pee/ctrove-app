using CTrove.Core.Enum;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Entity;
using System.Text.Json;
using System.Net;

namespace CTrove.Core
{
    public class AuditEntry
    {
        private EntityEntry _entityEntry;
        public Guid UserId { get; set; }
        public string TableName { get; set; }
        public string Remarks { get; set; }

        public string Location { get; set; }
        public AuditType AuditType { get; set; }
        public List<Dictionary<string, object>> ListValues { get; } = new List<Dictionary<string, object>>();

        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();

        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();

        public string TablePKey { get; set; }

        //public string KeyValues { get;set; }

        public List<string> ChangedColumns { get; } = new List<string>();
        public AuditEntry(EntityEntry entity)
        {
            _entityEntry = entity;
        }

        public Audit ToAudit()
        {
            Audit auditTrail = new Audit
            {
                Id = Guid.NewGuid(),
                PerformedBy = UserId,
                Table = TableName,
                DatePerformed = DateTime.Now.ToUniversalTime(),
                AuditType = AuditType,
                Remarks = Remarks,
                Location = Location,
                FromValue = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
                ToValue = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
                //TablePrimaryKey =  JsonSerializer.Serialize(KeyValues),
                RecordId = new Guid(TablePKey),
                AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)

            };
            return auditTrail;
        }

        


    }
}
