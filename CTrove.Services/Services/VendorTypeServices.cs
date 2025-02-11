using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using CTrove.Services.Extensions;
using CTrove.Core.Entity;

namespace CTrove.Services.Services
{
    public interface IVendorTypeServices
    {
        Task<PagedResult<VendorTypeResponse>> GetPagedList(BaseFilters filters);
        Task<IEnumerable<VendorTypeResponse>> GetList();
        Task<VendorTypeResponse?> Get(Guid id, Guid objId);
        Task<VendorTypeResponse?> Add(VendorTypeRequest req, Guid objId);
        Task<VendorTypeResponse?> Update(VendorTypeRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);
    }
    public class VendorTypeServices : IVendorTypeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public VendorTypeServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<PagedResult<VendorTypeResponse>> GetPagedList(BaseFilters filters)
            => _unitOfWork._VendorType.GetDbSet()
            .AsNoTracking()
            .AsEnumerable()
            .Where(f => (f.Code.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) ||
                    f.Name.Contains(filters.Search, StringComparison.OrdinalIgnoreCase))
                    && f.Status == filters.Status)
            .ToVendorTypeResponseList()
            .ToPagedList(filters.Page, filters.Limit);

        public async Task<IEnumerable<VendorTypeResponse>> GetList()
            => _unitOfWork._VendorType.GetDbSet()
                    .AsNoTracking()
                    .ToVendorTypeResponseList()
                    .ToList();

        public async Task<VendorTypeResponse?> Get(Guid id, Guid objId)
        {
            var entity = await _unitOfWork._VendorType.GetDbSet()
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

            return entity.ToVendorTypeResponse();
        }

        public async Task<VendorTypeResponse?> Add(VendorTypeRequest req, Guid objId)
        {
            if (req == null) return null;

            var entity = new VendorType
            {
                Id = Guid.NewGuid(),
                Code = req.Code,
                Name = req.Name,
                Status = req.Status,
            };

            await _unitOfWork._VendorType.Add(entity);
            int result = await _unitOfWork.SaveData(
                  userId: objId,
                  remarks: req.Remarks ?? "",
                  location: req.Location ?? ""
                  );

            return result > 0 ? entity.ToVendorTypeResponse() : null;
        }

        public async Task<VendorTypeResponse?> Update(VendorTypeRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = await _unitOfWork._VendorType.GetDbSet()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == req.Id);

            if (entity == null) return null;

            entity.Name = req.Name;
            entity.Status = req.Status;
            entity.Code = req.Code;

            await _unitOfWork._VendorType.Update(entity);
            int result = await _unitOfWork.SaveData(
                  userId: objId,
                  remarks: req.Remarks ?? "",
                  location: req.Location ?? ""
                  );

            return entity.ToVendorTypeResponse();
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._VendorType.GetDbSet().FirstOrDefaultAsync(f => f.Id == req.Id);

            if (entity == null) return false;
            entity.Status = false;

            await _unitOfWork._VendorType.Deactivate(entity);

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
