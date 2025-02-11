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
    public class VisitsServices : IVisitsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;

        public VisitsServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<PagedResult<VisitsResponse>> GetList(VisitsFilters filter)
        {
            return _unitOfWork._Visits.GetDbSet()
                .AsNoTracking()
                .AsQueryable()
                .Where(f => ( f.Code.Contains(filter.Search) || f.Name.Contains(filter.Search))
                    && f.Status == filter.Status
                    && (filter.VisitType > 0 ? f.VisitType == filter.VisitType : true))
                .ToList()
                .ToVisitsResponseList()
                .ToPagedList(filter.Page, filter.Limit);
        }

        public async Task<VisitsResponse> GetById(Guid objId, Guid id)
        {
            var entity = await _unitOfWork._Visits.GetById(id);
            if (entity == null) return null;

            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "Visits"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);
            // await _unitOfWork.auditTrailRetrieved(objId);
            return entity.ToVisitsResponse();
        }

        public async Task<VisitsResponse> Save(Guid objId, VisitsRequest req)
        {
            var visits = new Visits
            {
                Id = Guid.NewGuid(),
                Status = req.Status,
                Code = req.Code,
                isRequired = req.isRequired,
                Name = req.Name,
                TargetDays = req.TargetDays,
                TimeWindow = req.TimeWindow,
                VisitId = req.VisitId,
                VisitType = req.VisitType,
            };

            await _unitOfWork._Visits.Add(visits);
            var res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return visits.ToVisitsResponse();

            return null;
        }

        public async Task<VisitsResponse> Update(Guid objId, VisitsRequest req)
        {
            if (req is null) return null;

            var _visits = await _unitOfWork._Visits.GetById(req.Id);
            if (_visits is null) return null;

            _visits.Status = req.Status;
            _visits.Code = req.Code;
            _visits.isRequired = req.isRequired;
            _visits.Name = req.Name;
            _visits.TargetDays = req.TargetDays;
            _visits.TimeWindow = req.TimeWindow;
            _visits.VisitId = req.VisitId;
            _visits.VisitType = req.VisitType;

            await _unitOfWork._Visits.Update(_visits);
            int result = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (result > 0) return _visits.ToVisitsResponse(); else return null;
        }

        public async Task<bool> Deactivate(Guid objId, DeactivateRequest req)
        {
            if (req.Id == Guid.Empty) return false;

            var _visits = await _unitOfWork._Visits.GetById(req.Id);
            if (_visits == null) return false;

            _visits.Status = false;
            await _unitOfWork._Visits.Deactivate(_visits);
            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0)
                return true;
            else return false;

        }

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._Visits.GetDbSet()
                .FirstOrDefaultAsync(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim());

            return result != null ? true : false;
        }

    }
}
