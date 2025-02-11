using CTrove.Core.Common;
using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using CTrove.Core.Enum;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Extensions;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CTrove.Services.Services
{
    public class AuditTrailServices : IAuditTrailServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeZoneServices _timeZoneServices;
        public AuditTrailServices(IUnitOfWork unitOfWork, ITimeZoneServices timeZoneServices)
        {
            _unitOfWork = unitOfWork;
            _timeZoneServices = timeZoneServices;
        }
        public async Task<IEnumerable<AuditResponse>?> GetAuditListByRecordId(Guid recordId, Guid objcId)
        {
            var settings = await _unitOfWork._Settings.GetDbSet()
                .AsNoTracking()
                .Include(f => f.User)
                .Where(f => f.User.ObjectId == objcId)
                .FirstOrDefaultAsync();

            return _unitOfWork._Audit.GetDbSet()
                .AsNoTracking()
                .AsEnumerable()
                .Where(f => f.RecordId == recordId)
                .Select(e => new Audit
                {
                    Id = e.Id,
                    AffectedColumns = e.AffectedColumns,
                    AuditType = e.AuditType,
                    DatePerformed = _timeZoneServices.ConvertToUserTimeZoneSettings(e.DatePerformed, settings!.TimeZone),
                    FromValue = e.FromValue,
                    Location = e.Location,
                    PerformedBy = e.PerformedBy,
                    RecordId = recordId,
                    Remarks = e.Remarks,
                    Table = e.Table,
                    ToValue = e.ToValue
                })
                .OrderByDescending(f => f.DatePerformed)
                .ToAuditResponseList();
        }

        public async Task<IEnumerable<AuditResponse>?> GetAuditListByRecordIdWithUser(Guid recordId, Guid objcId)
        {
            var settings = await _unitOfWork._Settings.GetDbSet()
                .AsNoTracking()
                .Include(f => f.User)
                .Where(f => f.User.ObjectId == objcId)
                .FirstOrDefaultAsync();

            var audit = await _unitOfWork._Audit.GetDbSet()
                .AsNoTracking()
                .Where(f => f.RecordId == recordId)
                .OrderByDescending(f => f.DatePerformed)
                .ToListAsync();

            if (audit == null) return null;

            return audit.Select(e => new AuditResponseWithUser
            {
                Audit = new Audit
                {
                    Id = e.Id,
                    AffectedColumns = e.AffectedColumns,
                    AuditType = e.AuditType,
                    DatePerformed = _timeZoneServices.ConvertToUserTimeZoneSettings(e.DatePerformed, settings!.TimeZone),
                    FromValue = e.FromValue,
                    Location = e.Location,
                    PerformedBy = e.PerformedBy,
                    RecordId = recordId,
                    Remarks = e.Remarks,
                    Table = e.Table,
                    ToValue = e.ToValue
                },
                User = _unitOfWork._User.GetDbSet()
                    .AsNoTracking()
                    .FirstOrDefault(f => f.ObjectId == e.PerformedBy)
            })
            .ToAuditResponseWithUserList();
        }

        public async Task<Audit> PerformAuditTrailforRetrieved(AuditRetrievedRequest req)
        {
            Audit audit = new Audit
            {
                Id = Guid.NewGuid(),
                ToValue = JsonSerializer.Serialize(req.obj),
                AuditType = req.AuditType,
                PerformedBy = req.PerformedBy,
                DatePerformed = DateTime.Now.ToUniversalTime(),
                RecordId = req.recordId,
                Remarks = "",
                Location = "",
                Table = req.Table,
            };

            await _unitOfWork._Audit.Add(audit);
            int res = _unitOfWork.Save();

            return res > 0 ? audit : null!;
        }

    }
}
