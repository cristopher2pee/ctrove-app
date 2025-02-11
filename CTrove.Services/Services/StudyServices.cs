using Ctrove.HR.Services;
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
    public class StudyServices : IStudyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrAuthenticationServices _hrAuthenticationServices;

        public StudyServices(IUnitOfWork unitOfwork, IAuditTrailServices auditTrailServices, IHrAuthenticationServices hrAuthenticationServices)
        {
            _unitOfWork = unitOfwork;
            _auditTrailServices = auditTrailServices;
            _hrAuthenticationServices = hrAuthenticationServices;
        }

        public async Task<StudyResponse?> GetById(Guid objId, Guid id)
        {
            var entity = await _unitOfWork._Study.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Id == id)
                .Include(f => f.Classification)
                .Include(f => f.TherapeuticArea)
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "Study"
            });
            return entity.ToStudyResponse();
        }

        public async Task<PagedResult<StudyResponse>> GetAll(BaseFilters filters)
        {
            var entities = _unitOfWork._Study.GetDbSet()
                .AsNoTracking()
                .Include(f => f.Classification)
                .Include(f => f.TherapeuticArea)
                .AsEnumerable()
                .Where(f => (f.Name.Contains(filters.Search) ||
                    f.Code.Contains(filters.Search))
                    && f.Status == filters.Status)
                .ToList();

            return entities.ToStudyResponseList().ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<StudyResponse?> Save(Guid objId, StudyRequest req)
        {
            if (req == null) return null;
            var entity = new Study
            {
                Id = Guid.NewGuid(),
                Status = req.Status,
                Name = req.Name,
                Code = req.Code,
                Sponsor = req.Sponsor,
                BillingCode = req.BillingCode,
                StudyType = req.StudyType,
                TherapeuticAreaId = req.TherapeuticAreaId,
                ClassificationId = req.ClassificationId,
            };

            var resultToken = await _hrAuthenticationServices.GetTokenResponse(objId, entity.Id);
            if (resultToken != null)
                entity.ApiKeyToken = resultToken!.apiToken;

            await _unitOfWork._Study.Add(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToStudyResponse(); else return null;
        }

        public async Task<StudyResponse?> Update(Guid objId, StudyRequest req)
        {
            if (req == null) return null;

            var entity = await _unitOfWork._Study.GetById(req.Id);
            if (entity is null) return null;

            entity.Status = req.Status;
            entity.Name = req.Name;
            entity.Code = req.Code;
            entity.Sponsor = req.Sponsor;
            entity.BillingCode = req.BillingCode;
            entity.StudyType = req.StudyType;
            entity.TherapeuticAreaId = req.TherapeuticAreaId;
            entity.ClassificationId = req.ClassificationId;

            var resultToken = await _hrAuthenticationServices.GetTokenResponse(objId, entity.Id);
            if (resultToken != null)
                entity.ApiKeyToken = resultToken!.apiToken;

            await _unitOfWork._Study.Update(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToStudyResponse(); else return null;
        }

        public async Task<bool> Deactivate(Guid objId, DeactivateRequest req)
        {
            if (req.Id == Guid.Empty) return false;
            var entity = await _unitOfWork._Study.GetById(req.Id);
            if (entity is null) return false;

            entity.Status = false;
            await _unitOfWork._Study.Deactivate(entity);
            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) return true; else return false;
        }

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._Study.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }
    }
}
