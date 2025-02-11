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
    public class StudyCountryServices : IStudyCountryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public StudyCountryServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<StudyCountry?> Save(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            StudyCountry studyCountry = new StudyCountry
            {
                Id = Guid.NewGuid(),
                Status = entity.Status,
                Code = entity.Code,
                Name = entity.Name,
            };

            //entity.Id = Guid.NewGuid();
            await _unitOfWork._StudyCountry.Add(studyCountry);

            Guid guid = Guid.NewGuid();
            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return studyCountry; else return null;


        }
        public async Task<StudyCountry?> Update(DefaultRequest entity, Guid userId)
        {
            if (entity == null) return null;

            var _entity = await _unitOfWork._StudyCountry.GetById(entity.Id);
            if (_entity == null) return null;

            _entity.Status = entity.Status;
            _entity.Name = entity.Name;
            _entity.Code = entity.Code;

            await _unitOfWork._StudyCountry.Update(_entity);

            Guid guid = Guid.NewGuid();
            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return _entity; else return null;
        }

        public async Task<bool> Delete(DeactivateRequest req, Guid userId)
        {
            if (req.Id == Guid.Empty) return false;

            var entity = await _unitOfWork._StudyCountry.GetById(req.Id);
            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._StudyCountry.Deactivate(entity);

            Guid guid = Guid.NewGuid();
            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) return true; else return false;

        }
        public async Task<StudyCountry?> GetById(Guid guid, Guid userId)
        {
            var result = await _unitOfWork._StudyCountry.GetById(guid);
            if (result is null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = userId,
                recordId = result.Id,
                Table = "StudyCountry"
            });

            return result;
        }
        public async Task<PagedResult<StudyCountry>> GetAll(BaseFilters filters)
        {
            return _unitOfWork._StudyCountry.GetDbSet()
                    .AsNoTracking()
                    .Where(f => (f.Code.Contains(filters.Search)
                        || f.Name.Contains(filters.Search)
                        && f.Status == filters.Status))
                    .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<bool> IsExist(string param)
        {

            var result = await _unitOfWork._StudyCountry.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }

    }
}
