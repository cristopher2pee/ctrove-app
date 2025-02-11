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
    public class RaceServices : IRaceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;

        public RaceServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;

        }

        public async Task<Race?> Save(RaceRequest entity, Guid objId)
        {
            if (entity == null) return null;

            Race _entity = new Race
            {
                Id = Guid.NewGuid(),
                Status = entity.Status,
                Code = entity.Code,
                Name = entity.Name,
            };

            await _unitOfWork._Race.Add(_entity);
            int res = await _unitOfWork.SaveData(objId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (res > 0) return _entity; else return null;
        }

        public async Task<Race?> Update(RaceRequest entity, Guid objId)
        {
            var race = await _unitOfWork._Race.GetById(entity.Id);
            if (race is null) return null;

            race.Status = entity.Status;
            race.Code = entity.Code;
            race.Name = entity.Name;

            await _unitOfWork._Race.Update(race);
            int res = await _unitOfWork.SaveData(objId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (res > 0) return race;

            return null;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req.Id == Guid.Empty) return false;

            var entity = await _unitOfWork._Race.GetById(req.Id);
            if (entity is null) return false;

            entity.Status = true;
            await _unitOfWork._Race.Deactivate(entity);

            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0)
                return true;
            else return false;
        }

        public async Task<Race?> GetbyId(Guid id, Guid objId)
        {
            var result = await _unitOfWork._Race.GetById(id);
            if (result is null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = result.Id,
                Table = "Race"
            });
            return result;
        }
        public async Task<PagedResult<Race>> GetAll(BaseFilters filters)
        {
            return _unitOfWork._Race.GetDbSet()
                .AsNoTracking()
                .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search)
                    && f.Status == filters.Status))
                .ToPagedListQueryable(filters.Page, filters.Limit);
        }

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._Race.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }

    }
}
