using Ctrove.HR.Services;
using CTrove.Core.Common;
using CTrove.Core.DTO;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Extensions;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Services
{
    public interface IContactTypeServices
    {
        Task<PagedResult<ContactTypeResponse>> GetPagedList(BaseFilters filters);
        Task<IEnumerable<ContactTypeResponse>> GetList();
        Task<ContactTypeResponse?> Get(Guid id, Guid objId);
        Task<ContactTypeResponse?> Add(ContactTypeRequest req, Guid objId);
        Task<ContactTypeResponse?> Update(ContactTypeRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);
    }
    public class ContactTypeServices : IContactTypeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrContactTypeServices _hrContactTypeServices;
        public ContactTypeServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices, IHrContactTypeServices hrContactTypeServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
            _hrContactTypeServices = hrContactTypeServices;
        }

        public async Task<PagedResult<ContactTypeResponse>> GetPagedList(BaseFilters filters)
            => _unitOfWork._ContactType.GetDbSet()
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(f => (f.Code.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) ||
                            f.Name.Contains(filters.Search, StringComparison.OrdinalIgnoreCase))
                            && f.Status == filters.Status)
                    .ToContactTypeResponseList()
                    .ToPagedList(filters.Page, filters.Limit);

        public async Task<IEnumerable<ContactTypeResponse>> GetList()
            => _unitOfWork._ContactType.GetDbSet()
                    .AsNoTracking()
                    .ToContactTypeResponseList()
                    .ToList();

        public async Task<ContactTypeResponse?> Get(Guid id, Guid objId)
        {
            var entity = await _unitOfWork._ContactType.GetDbSet()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(
                new Core.DTO.Response.AuditRetrievedRequest
                {
                    obj = entity,
                    AuditType = Core.Enum.AuditType.View,
                    PerformedBy = objId,
                    recordId = entity.Id,
                    Table = "ContactType"
                });

            return entity.ToContactTypeResponse();
        }

        public async Task<ContactTypeResponse?> Add(ContactTypeRequest req, Guid objId)
        {
            if (req == null) return null;

            var entity = new ContactType
            {
                Id = Guid.NewGuid(),
                Code = req.Code,
                Name = req.Name,
                Status = req.Status,
            };

            await _unitOfWork._ContactType.Add(entity);
            int result = await _unitOfWork.SaveData(
                  userId: objId,
                  remarks: req.Remarks ?? "",
                  location: req.Location ?? ""
                  );

            await _hrContactTypeServices.Add(new Core.DTO.HR.HrContactTypeRequest
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Status = entity.Status,
                UserObjectId = objId,
                Location = req.Location,
                Remarks = req.Remarks
            });

            return result > 0 ? entity.ToContactTypeResponse() : null;
        }

        public async Task<ContactTypeResponse?> Update(ContactTypeRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = await _unitOfWork._ContactType.GetDbSet()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == req.Id);

            if (entity == null) return null;

            entity.Name = req.Name;
            entity.Status = req.Status;
            entity.Code = req.Code;

            await _unitOfWork._ContactType.Update(entity);
            int result = await _unitOfWork.SaveData(
                  userId: objId,
                  remarks: req.Remarks ?? "",
                  location: req.Location ?? ""
                  );

            return entity.ToContactTypeResponse();
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._ContactType.GetDbSet().FirstOrDefaultAsync(f => f.Id == req.Id);

            if (entity == null) return false;
            entity.Status = false;

            await _unitOfWork._ContactType.Deactivate(entity);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? "",
                    isDelete: true
                );

            return result > 0 ? true : false;

        }
    }
}
