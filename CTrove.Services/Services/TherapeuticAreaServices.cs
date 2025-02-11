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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Services
{
    public class TherapeuticAreaServices : ITherapeuticAreaServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public TherapeuticAreaServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<TherapeuticArea?> Save(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            //entity.Id = Guid.NewGuid();

            TherapeuticArea therapeuticArea = new TherapeuticArea
            {
                Id = Guid.NewGuid(),
                Status = entity.Status,
                Code = entity.Code,
                Name = entity.Name,
            };
            
            await _unitOfWork._TherapeuticArea.Add(therapeuticArea);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return therapeuticArea; else return null;


        }
        public async Task<TherapeuticArea?> Update(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            var _entity = await _unitOfWork._TherapeuticArea.GetById(entity.Id);
            if (_entity == null) return null;

            _entity.Status = entity.Status;
            _entity.Name = entity.Name;
            _entity.Code = entity.Code;

            await _unitOfWork._TherapeuticArea.Update(_entity);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return _entity; else return null;
        }

        public async Task<bool> Delete(DeactivateRequest req, Guid userId)
        {
            if (req.Id == Guid.Empty) return false;

            var _entity = await _unitOfWork._TherapeuticArea.GetById(req.Id);
            if (_entity == null) return false;

            _entity.Status = false;
            await _unitOfWork._TherapeuticArea.Deactivate(_entity);

            Guid guid = Guid.NewGuid();
            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) return true; else return false;

        }
        public async Task<TherapeuticArea?> GetById(Guid guid, Guid userId)
        {
            var result = await _unitOfWork._TherapeuticArea.GetById(guid);
            if(result is null) return null;

            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = userId,
                recordId = result.Id,
                Table = "TherapeuticArea"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);
            //await _unitOfWork.auditTrailRetrieved(userId);
            return result;
        }

        public async Task<PagedResult<TherapeuticArea>> GetAll(BaseFilters filters)
        {
            var result = _unitOfWork._TherapeuticArea.GetDbSet()
                .AsQueryable()
                .AsNoTracking()
                .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search))
                    && f.Status == filters.Status)
                .ToPagedList(filters.Page, filters.Limit);

            return result;
        }

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._TherapeuticArea.GetDbSet()
                .FirstOrDefaultAsync(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim());

            return result != null ? true : false;
        }

    }
}
