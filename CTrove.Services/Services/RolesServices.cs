
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
    public class RolesServices : IRolesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public RolesServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<Roles?> Save(RolesRequest entity, Guid userId)
        {
            if (entity == null) return null;

            //entity.Id = Guid.NewGuid();
            Roles roles = new Roles
            {
                Id = Guid.NewGuid(),
                Name = entity.Name,
                Code = entity.Code,
                Status = entity.Status,
                Blinded = entity.Blinded,
                RolesPages = entity.RolesPages.Select(e => new RolesPages
                {
                    Pages = e.Pages,
                    Status = e.Status,
                }).ToList()
            };

            await _unitOfWork._Roles.Add(roles);

            Guid guid = Guid.NewGuid();
            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return roles; else return null;


        }
        public async Task<Roles?> Update(RolesRequest entity, Guid userId)
        {
            if (entity == null) return null;

            var _entity = await _unitOfWork._Roles.GetDbSet()
                .Include(f => f.RolesPages.Where(f => f.Status == true))
                .Where(f => f.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (_entity == null) return null;

            _entity.Status = entity.Status;
            _entity.Name = entity.Name;
            _entity.Code = entity.Code;
            _entity.Blinded = entity.Blinded;

            if (entity.RolesPages is not null)
            {
                foreach (var e in entity.RolesPages)
                {
                    if (e.Id == Guid.Empty)
                    {
                        _entity.RolesPages.Add(e);
                    }
                    else
                    {
                        var rolesPage = _entity.RolesPages.FirstOrDefault(f => f.Id == e.Id);
                        if (rolesPage != null)
                        {
                            rolesPage.Pages = e.Pages;
                            rolesPage.Status = e.Status;
                        }
                    }
                }
            }

            await _unitOfWork._Roles.Update(_entity);
            int result = await _unitOfWork.SaveData(userId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (result > 0) return _entity; else return null;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid userId)
        {
            if (req.Id == Guid.Empty) return false;

            var entity = await _unitOfWork._Roles.GetDbSet()
                .AsNoTracking()
                .Include(f => f.RolesPages)
                .Where(f => f.Id == req.Id)
                .FirstOrDefaultAsync();

            if (entity == null) return false;
            entity.Status = false;

            if (entity.RolesPages != null && entity.RolesPages.Any())
            {
                foreach (var e in entity.RolesPages)
                {
                    e.Status = false;
                }
            }

            await _unitOfWork._Roles.Deactivate(entity);

            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "",
                location: req.Location ?? "") > 0) return true;
            else return false;

        }
        public async Task<Roles?> GetById(Guid guid, Guid userId)
        {
            var result = await _unitOfWork._Roles.GetDbSet()
                .AsNoTracking()
                .Include(f => f.RolesPages.Where(f => f.Status == true))
                .Where(f => f.Id == guid).FirstOrDefaultAsync();

            if (result is null) return null;

            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = userId,
                recordId = result.Id,
                Table = "Roles"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);

            // await _unitOfWork.auditTrailRetrieved(userId);
            return result;
        }
        public async Task<PagedResult<RolesResponse>> GetAll(RolesFilters filters)
        {
            return _unitOfWork._Roles.GetDbSet()
                .AsNoTracking()
                .Include(f => f.RolesPages.Where(f => f.Status == true))
                .Where(f => (f.Code.Contains(filters.Search)
                    || f.Name.Contains(filters.Search))
                    && f.Status == filters.Status
                    && f.Blinded == filters.isBlinded)
                .OrderBy(f => f.Code)
                .ToRolesListResponse()
                .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<List<Roles>> GetList()
            => await _unitOfWork._Roles.GetDbSet()
            .AsNoTracking()
            .Include(f => f.RolesPages.Where(f => f.Status == true))
            .Where(r => r.Status).ToListAsync();

        public async Task<bool> IsExist(string param)
        {
            //var result = await _unitOfWork._Roles.GetDbSet()
            //    .FirstOrDefaultAsync(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
            //    || f.Name.ToUpper().Trim() == param.ToUpper().Trim());

            //return result != null ? true : false;

            var result = await _unitOfWork._Roles.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }
    }
}
