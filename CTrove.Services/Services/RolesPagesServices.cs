using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
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
    public class RolesPagesServices : IRolesPagesServices
    {
        private IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public RolesPagesServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }
        public async Task<RolesPages?> Save(RolesPagesRequest req, Guid objId)
        {
            if (req == null) return null;

            RolesPages entity = new RolesPages
            {
                Id = Guid.NewGuid(),
                Pages = req.Pages,
                RolesId = req.RolesId
            };

            await _unitOfWork._RolesPages.Add(entity);

            int result = await _unitOfWork.SaveData(objId, remarks: req.remarks ?? "", location: req.location ?? "");
            if (result > 0) return entity; else return null;
        }
        public async Task<RolesPages?> Update(RolesPagesRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = await _unitOfWork._RolesPages.GetDbSet()
                .AsNoTracking()
                .FirstOrDefaultAsync(f=>f.Id ==  req.Id);

            if (entity is null) return null;

            entity.Pages = req.Pages;
            entity.RolesId = req.RolesId;

            await _unitOfWork._RolesPages.Update(entity);

            int res = await _unitOfWork.SaveData(objId, remarks: req.remarks ?? "", location: req.location ?? "");
            if (res > 0) return entity;

            return null;
        }
        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req.Id == Guid.Empty) return false;

            var result = await _unitOfWork._RolesPages.GetById(req.Id);
            if (result == null) return false;

            result.Status = false;
            await _unitOfWork._RolesPages.Deactivate(result);

            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0)
                return true;
            else return false;
        }
        public async Task<RolesPages?> GetById(Guid id, Guid objId)
        {
            var result = await _unitOfWork._RolesPages.GetById(id);
            if (result is null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = result.Id,
                Table = "RolesPages"
            });
            return result;
        }

        public async Task<PagedResult<RolesPages>> GetAll(RolesPagesFilters req)
        {
            var result = _unitOfWork._RolesPages.GetDbSet()
                .AsNoTracking()
                .Where(f => f.RolesId == req.RolesId)
                .ToPagedList(req.Page, req.Limit);

            return result;
        }
    }
}
