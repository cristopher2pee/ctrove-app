using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class AuditResponseExtension
    {
        public static AuditResponse ToAuditResponse(this Audit entity)
        {
            return new AuditResponse
            {
                Id = entity.Id,
                PerformedBy = entity.PerformedBy,
                AffectedColumn = entity.AffectedColumns ?? "",
                AuditType = entity.AuditType,
                DatePeformed = entity.DatePerformed,
                FromValue = entity.FromValue ?? "",
                ToValue = entity.ToValue??"",
                RecordId = entity.RecordId,
                Table = entity.Table ?? "",
                Location = entity.Location ?? "",
                Remarks = entity.Remarks ?? ""
            };
        }

        public static IEnumerable<AuditResponse> ToAuditResponseList(this IEnumerable<Audit> entities)
            => entities.Select(f => f.ToAuditResponse()).ToList();

        public static AuditResponse ToAuditResponseWithUser(this AuditResponseWithUser e)
        {
            return new AuditResponse
            {
                Id = e.Audit!.Id,
                PerformedBy = e.Audit.PerformedBy,
                UserResponse = e.User!.ToUserResponse(),
                AffectedColumn = e.Audit.AffectedColumns!,
                AuditType = e.Audit.AuditType,
                DatePeformed = e.Audit.DatePerformed,
                FromValue = e.Audit.FromValue!,
                ToValue = e.Audit.ToValue!,
                RecordId = e.Audit.RecordId,
                Table = e.Audit.Table,
                Location = e.Audit.Location!,
                Remarks = e.Audit.Remarks
            };
        }

        public static IEnumerable<AuditResponse> ToAuditResponseWithUserList(this IEnumerable<AuditResponseWithUser> entities)
            => entities.Select(f => f.ToAuditResponseWithUser()).ToList();
    }
}
