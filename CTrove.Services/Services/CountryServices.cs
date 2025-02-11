using CTrove.Core.Common;
using CTrove.Core.DTO;
using CTrove.Core.DTO.Request;
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
    public interface ICountryServices
    {
        Task<IEnumerable<CountryResponse>> GetList();
        Task<PagedResult<CountryResponse>> GetPagedList(BaseFilters filters);
        Task<CountryResponse?> Get(Guid id, Guid objId);
        Task<CountryResponse?> Add(CountryRequest req, Guid objId);
        Task<CountryResponse?> Update(CountryRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);
        Task<bool> isCountryExist(string param);

    }
    public class CountryServices : ICountryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public CountryServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }
        public async Task<IEnumerable<CountryResponse>> GetList()
        {
            var entities = _unitOfWork._Country.GetDbSet()
                .AsNoTracking()
                .AsEnumerable()
                .ToCountryResponseList();

            return entities;
        }

        public async Task<bool> isCountryExist(string param)
        {
            var result = await _unitOfWork._Country.GetDbSet()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim() ||
                f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._Country.GetDbSet().FirstOrDefaultAsync(f => f.Id == req.Id);
            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._Country.Deactivate(entity);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? "",
                    isDelete: true
                );

            return result > 0 ? true : false;

        }

        public async Task<PagedResult<CountryResponse>> GetPagedList(BaseFilters filters)
        {
            var entities = _unitOfWork._Country.GetDbSet()
                .AsNoTracking()
                .AsEnumerable()
                .Where(f => (f.Code.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) ||
                    f.Name.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) ||
                    f.Continent.Contains(filters.Search, StringComparison.OrdinalIgnoreCase))
                    && f.Status == filters.Status)
                .ToCountryResponseList()
                .OrderBy(f => f.Name)
                .ToPagedList(filters.Page, filters.Limit);

            return entities;
        }

        public async Task<CountryResponse?> Add(CountryRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = new Country
            {
                Id = Guid.NewGuid(),
                Code = req.Code,
                Status = req.Status,
                Continent = req.Continent,
                Name = req.Name,
            };

            await _unitOfWork._Country.Add(entity);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? ""
                    );

            return result > 0 ? entity.ToCountryResponse() : null;

        }

        public async Task<CountryResponse?> Update(CountryRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = await _unitOfWork._Country.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == req.Id);
            if (entity == null) return null;

            entity.Code = req.Code;
            entity.Continent = req.Continent;
            entity.Status = req.Status;
            entity.Name = req.Name;

            await _unitOfWork._Country.Update(entity);
            int result = await _unitOfWork.SaveData(
                 userId: objId,
                 remarks: req.Remarks ?? "",
                 location: req.Location ?? ""
                 );

            return result > 0 ? entity.ToCountryResponse() : null;
        }

        public async Task<CountryResponse?> Get(Guid id, Guid objId)
        {
            var entity = await _unitOfWork._Country.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(
                new Core.DTO.Response.AuditRetrievedRequest
                {
                    obj = entity,
                    AuditType = Core.Enum.AuditType.View,
                    PerformedBy = objId,
                    recordId = entity.Id,
                    Table = "Country"
                });

            return entity.ToCountryResponse();
        }

    }
}
