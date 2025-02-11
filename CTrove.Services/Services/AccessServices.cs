using CTrove.Core.DTO.Response;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Core.Common;
using CTrove.Services.Extensions;
using CTrove.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using System.Runtime.InteropServices.JavaScript;
using System.Net;
using System.Security.Cryptography;

namespace CTrove.Services.Services
{
    public class AccessServices : IAccessServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;

        public AccessServices(IUnitOfWork unitOfwork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfwork;
            _auditTrailServices = auditTrailServices;
        }

        public async Task<AccessListResponse?> GetById(Guid id, Guid objectId)
        {
            var entity = await _unitOfWork._Access.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Id == id)
                .Include(f => f.User)
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            var accessListRes = new AccessListResponse()
            {
                AccessResponse = entity.ToAccessResponse(),
                SitesResponse = _unitOfWork._Sites.GetDbSet()
                          .AsNoTracking()
                          .FirstOrDefaultAsync(f => f.Id == entity.AccessLevelId).Result?.ToSitesResponse(),
                StudyCountry = await _unitOfWork._StudyCountry.GetDbSet()
                          .AsNoTracking()
                          .FirstOrDefaultAsync(f => f.Id == entity.AccessLevelId)

            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objectId,
                recordId = entity.Id,
                Table = "Access"
            });
            return accessListRes;
        }

        public async Task<PagedResult<AccessResponse>> GetAll(AccessFilters filters)
        {
            return _unitOfWork._Access.GetDbSet()
                .AsNoTracking()
                .Include(f => f.User)
                .AsEnumerable()
                .Where(e => (e.User.Lastname.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) ||
                    e.User.Firstname.Contains(filters.Search, StringComparison.OrdinalIgnoreCase)
                ) && e.Status == filters.Status)
                .ToAccessListResponse()
                .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<bool> IsAccessAlreadyExist(AccessFilters filters)
        {
            var accessList = await _unitOfWork._Access.GetDbSet()
                .Where(f => f.UserId == filters.userId
                    && f.AccessLevelId == filters.accessLevelId
                    && f.Rights == filters.Right).FirstOrDefaultAsync();
            if (accessList != null) return true; else return false;
        }
        public async Task<PagedResult<AccessListResponse>> GetListAccess(AccessFilters filters)
        {
            List<AccessListResponse> listResponse = new List<AccessListResponse>();

            var AccessList = await _unitOfWork._Access.GetDbSet()
                .AsNoTracking()
                .Include(f => f.User)
                .ToListAsync();

            bool isRead = Convert.ToBoolean(filters.Right & Convert.ToInt32(Math.Pow(2, 0)));
            bool isWrite = Convert.ToBoolean(filters.Right & Convert.ToInt32(Math.Pow(2, 1)));
            bool isBin = Convert.ToBoolean(filters.Right & Convert.ToInt32(Math.Pow(2, 2)));

           return AccessList.Select( e => new AccessListResponse
            {
                AccessResponse = e.ToAccessResponse(),
                SitesResponse = _unitOfWork._Sites.GetDbSet()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Id == e.AccessLevelId).Result?.ToSitesResponse(),
                StudyCountry =  _unitOfWork._StudyCountry.GetDbSet()
                        .AsNoTracking().FirstOrDefault(f => f.Id == e.AccessLevelId)
            })
            .AsEnumerable()
            .Where(f => f.AccessResponse.Status == filters.Status
                    &&  (f.AccessResponse.User.Firstname.Contains(filters.Search, StringComparison.OrdinalIgnoreCase)
                    || f.AccessResponse.User.Lastname.Contains(filters.Search, StringComparison.OrdinalIgnoreCase)
                    || (f.SitesResponse != null ? f.SitesResponse.Code.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false)
                    || (f.SitesResponse != null ? f.SitesResponse.Name.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false)
                    || (f.StudyCountry != null ? f.StudyCountry.Code.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false)
                    || (f.StudyCountry != null ? f.StudyCountry.Name.Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false))
                    && (filters.Right > 0 ?
                        (Convert.ToBoolean(f.AccessResponse.Rights & Convert.ToInt32(Math.Pow(2, 0))) && isRead) ||
                         (Convert.ToBoolean(f.AccessResponse.Rights & Convert.ToInt32(Math.Pow(2, 1))) && isWrite) ||
                          (Convert.ToBoolean(f.AccessResponse.Rights & Convert.ToInt32(Math.Pow(2, 2))) && isBin) : true)
                    )
            .ToList()
            .ToPagedList(filters.Page, filters.Limit);

        }


        public async Task<AccessResponse?> Save(Guid objId, AccessRequest req)
        {
            if (req == null) return null;

            var access = new Access
            {
                Id = Guid.NewGuid(),
                Status = req.Status,
                AccessLevelId = req.AccessLevelId,
                Rights = req.Rights,
                UserId = req.UserId,
            };

            await _unitOfWork._Access.Add(access);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return access.ToAccessResponse(); else return null;

        }

        public async Task<AccessResponse?> Update(Guid objId, AccessRequest req)
        {
            if (req == null) return null;

            var entity = await _unitOfWork._Access.GetById(req.Id);
            if (entity is null) return null;

            entity.AccessLevelId = req.AccessLevelId;
            entity.UserId = req.UserId;
            entity.Rights = req.Rights;
            entity.Status = req.Status;

            await _unitOfWork._Access.Update(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToAccessResponse(); else return null;

        }

        public async Task<bool> Deactivate(Guid objId, DeactivateRequest req)
        {
            if (req.Id == Guid.Empty) return false;
            var entity = await _unitOfWork._Access.GetById(req.Id);
            if (entity is null) return false;

            entity.Status = false;
            await _unitOfWork._Access.Deactivate(entity);
            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "") > 0) 
                return true; else return false;
        }

    }
}
