using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Common;
using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using CTrove.Core.Filters;

namespace CTrove.Services.Interface
{
    public interface IAuditTrailServices
    {
        Task<IEnumerable<AuditResponse>?> GetAuditListByRecordId(Guid recordId, Guid objcId);

        Task<IEnumerable<AuditResponse>?> GetAuditListByRecordIdWithUser(Guid recordId, Guid objcId);

        Task<Audit> PerformAuditTrailforRetrieved(AuditRetrievedRequest req);

    }
}
