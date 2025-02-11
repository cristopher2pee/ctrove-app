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
    public class PhaseServices : IPhaseServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public PhaseServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<Phase?> Save(PhaseRequest entity, Guid userId)
        {
            if (entity == null) return null;

            Phase phase = new Phase
            {
                Id = Guid.NewGuid(),
                Code = entity.Code,
                Name = entity.Name,
                Status = entity.Status,
                PrevPhase = entity.PrevPhase
            };

            await _unitOfWork._Phase.Add(phase);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return phase; else return null;


        }
        public async Task<Phase?> Update(PhaseRequest entity, Guid userId)
        {
            if (entity == null) return null;

            var phase = await _unitOfWork._Phase.GetById(entity.Id);
            if (phase == null) return null;

            phase.Status = entity.Status;
            phase.Name = entity.Name;
            phase.Code = entity.Code;
            phase.PrevPhase = entity.PrevPhase;

            await _unitOfWork._Phase.Update(phase);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return phase; else return null;

        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid userId)
        {
            if (req.Id == Guid.Empty) return false;

            var phase = await _unitOfWork._Phase.GetById(req.Id);
            if (phase == null) return false;

            phase.Status = false;
            await _unitOfWork._Phase.Deactivate(phase);

            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) 
                return true; else return false;

        }
        public async Task<Phase?> GetById(Guid guid, Guid userId)
        {
            var result = await _unitOfWork._Phase.GetById(guid);
            if (result is null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved( new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = userId,
                recordId = result.Id,
                Table = "Phase"
            });
            //await _unitOfWork.auditTrailRetrieved(userId);
            return result;
        }
        public async Task<PagedResult<Phase>> GetAll(BaseFilters filters)
        {
            return _unitOfWork._Phase.GetDbSet()
                .AsNoTracking()
                .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search)
                    && f.Status == filters.Status))
                .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._Phase.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }
    }
}
