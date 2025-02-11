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
    public class ClassificationServices : IClassificationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrCountryServices _countryServices;

        public ClassificationServices(IUnitOfWork unitOfWork,
            IAuditTrailServices auditTrailServices,
            IHrCountryServices countryServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
            _countryServices = countryServices;
        }

        public async Task<Classification?> Save(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            Classification classification = new Classification()
            {
                Id = Guid.NewGuid(),
                Code = entity.Code,
                Name = entity.Name,
                Status = entity.Status,
            };

            await _unitOfWork._Classification.Add(classification);
            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return classification; else return null;


        }
        public async Task<Classification?> Update(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            var classification = await _unitOfWork._Classification.GetById(entity.Id);
            if (classification == null) return null;

            classification.Status = entity.Status;
            classification.Name = entity.Name;
            classification.Code = entity.Code;

            await _unitOfWork._Classification.Update(classification);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return classification; else return null;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid userId)
        {
            if (req.Id == Guid.Empty) return false;

            var classification = await _unitOfWork._Classification.GetById(req.Id);
            if (classification == null) return false;

            classification.Status = false;
            await _unitOfWork._Classification.Deactivate(classification);
            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) 
                return true; else return false;

        }
        public async Task<Classification?> GetById(Guid guid, Guid objId)
        {
            var result = await _unitOfWork._Classification.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Id == guid)
                .FirstOrDefaultAsync();

            if (result is null) return null;
            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = result.Id,
                Table = "Classification"
            });

            return result;
        }
        public async Task<PagedResult<Classification>> GetAll(BaseFilters filters)
        {

            return _unitOfWork._Classification.GetDbSet()
                .AsNoTracking()
                .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search)
                    && f.Status == filters.Status))
                .ToPagedList(filters.Page, filters.Limit);

        } 

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._Classification.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }


    }
}
