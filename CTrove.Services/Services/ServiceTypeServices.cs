using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Interface;
using CTrove.Services.Interface;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using CTrove.Core.Common;
using CTrove.Services.Extensions;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace CTrove.Services.Services
{
    public class ServiceTypeServices : IServiceTypesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;

        public ServiceTypeServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<ServiceType?> Save(DefaultRequest entity, Guid userId)
        {
            if (entity is null) return null;

            ServiceType serviceType = new ServiceType
            {
                Id = Guid.NewGuid(),
                Status = entity.Status,
                Code = entity.Code,
                Name = entity.Name,
            };

            await _unitOfWork._ServiceTypes.Add(serviceType);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return serviceType; else return null;

        }

        public async Task<ServiceType?> Update(DefaultRequest entity, Guid userId)
        {
            if (entity is null) return null;

            var _serviceType = await _unitOfWork._ServiceTypes.GetById(entity.Id);
            if (_serviceType == null) return null;

            _serviceType.Status = entity.Status;
            _serviceType.Code = entity.Code;
            _serviceType.Name = entity.Name;

            await _unitOfWork._ServiceTypes.Update(_serviceType);

            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return _serviceType; else return null;
        }
        public async Task<bool> Deactivate(DeactivateRequest req, Guid userId)
        {
            if (req.Id != Guid.Empty)
            {
                var serviceType = await _unitOfWork._ServiceTypes.GetById(req.Id);
                if (serviceType == null) return false;

                serviceType.Status = false;
                await _unitOfWork._ServiceTypes.Deactivate(serviceType);

                Guid guid = Guid.NewGuid();
                if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) return true; else return false;
            }
            return false;
        }

        public async Task<ServiceType?> GetById(Guid id, Guid userId)
        {
            var returnData = await _unitOfWork._ServiceTypes.GetById(id);
            if (returnData is null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = returnData,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = userId,
                recordId = returnData.Id,
                Table = "ServiceType"
            });

            return returnData;
        }

        public async Task<IEnumerable<ServiceType>> GetAll()
        {
            var returnValue = await _unitOfWork._ServiceTypes.GetAll();
            return returnValue;
        }

        public async Task<PagedResult<ServiceType>> GetAllServiceType(BaseFilters filters)
            => _unitOfWork._ServiceTypes.GetDbSet()
            .AsNoTracking()
            .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search))
                    && f.Status == filters.Status)
            .ToPagedList(filters.Page, filters.Limit);

        public async Task<bool> IsExist(string param)
        {
            var result = await _unitOfWork._ServiceTypes.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }


    }
}
