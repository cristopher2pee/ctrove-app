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
    public class SitesServices : ISitesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeZoneServices _timeZoneServices;
        private readonly ISettingsServices _settingsServices;
        private string timeZone = "Asia/Singapore";
        private readonly IAuditTrailServices _auditTrailServices;
        public SitesServices(IUnitOfWork unitOfWork, ITimeZoneServices timeZoneServices, ISettingsServices settingsServices, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _timeZoneServices = timeZoneServices;
            _settingsServices = settingsServices;
            _auditTrailServices = auditTrailServices;
        }
        public async Task<SitesResponseDto?> GetbyId(Guid objId, Guid Id)
        {

            var _user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == objId).FirstOrDefaultAsync();

            if (_user != null)
            {
                var settingTimeZone = await _settingsServices.GetTimeZoneSetting(_user.Id);
                if (settingTimeZone != null) timeZone = settingTimeZone.ToString();
            }

            var entity = await _unitOfWork._Sites.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Id == Id)
                .Include(f => f.StudyCountry)
                .Include(f => f.ServiceType)
                .Include(f => f.SitePhases)
                .Select(e => new SitesResponseDto
                {
                    Id = e.Id,
                    ServiceType = e.ServiceType,
                    ServiceTypeId = e.ServiceTypeId,
                    StudyCountry = e.StudyCountry,
                    StudyCountryId = e.StudyCountryId,
                    SiteStatus = e.SiteStatus,
                    Status = e.Status,
                    Code = e.Code,
                    Name = e.Name,
                    StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.StartDate, timeZone),
                    EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.EndDate, timeZone),
                    SitePhases = e.SitePhases.Where(f => f.Status == true)
                        .Select(s => new SitePhasesResponseDto
                        {
                            Id = s.Id,
                            SitesId = s.SitesId,
                            Status = s.Status,
                            Phase = s.Phase,
                            StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(s.StartDate, timeZone),
                            EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(s.EndDate, timeZone),
                            PhaseId = s.PhaseId,
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            await _auditTrailServices.PerformAuditTrailforRetrieved(new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "Sites"

            });
            return entity;
        }

        public async Task<PagedResult<SitesResponse>> GetAllPagedList(SitesFilters filters, Guid objId)
        {
            var _user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == objId).FirstOrDefaultAsync();

            if (_user != null)
            {
                var settingTimeZone = await _settingsServices.GetTimeZoneSetting(_user.Id);
                if (settingTimeZone != null) timeZone = settingTimeZone.ToString();
            }

            return _unitOfWork._Sites.GetDbSet()
                .AsNoTracking()
                .AsQueryable()
                .Include(f => f.StudyCountry)
                .Include(f => f.ServiceType)
                .Include(f => f.SitePhases.Where(f => f.Status == true)).ThenInclude(f => f.Phase)
                .Where(f =>
                        (
                                f.Code.Contains(filters.Search)
                                || f.Name.Contains(filters.Search)
                                || f.StudyCountry.Code.Contains(filters.Search)
                                || f.StudyCountry.Name.Contains(filters.Search)
                                || f.ServiceType.Code.Contains(filters.Search)
                                || f.ServiceType.Name.Contains(filters.Search))
                        && f.Status == filters.Status
                        && (filters.SiteStatusId != null ? filters.SiteStatusId.Contains(f.SiteStatus) : true)
                        && (filters.StudyCountryId != null ? filters.StudyCountryId.Contains(f.StudyCountryId) : true)
                        && (filters.ServiceTypeId != null ? filters.ServiceTypeId.Contains(f.ServiceTypeId) : true)
                        )
                 .Select(e => new Sites
                 {
                     Id = e.Id,
                     ServiceType = e.ServiceType,
                     ServiceTypeId = e.ServiceTypeId,
                     StudyCountry = e.StudyCountry,
                     StudyCountryId = e.StudyCountryId,
                     SiteStatus = e.SiteStatus,
                     Status = e.Status,
                     Code = e.Code,
                     Name = e.Name,
                     StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.StartDate, timeZone),
                     EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.EndDate, timeZone),
                     SitePhases = e.SitePhases

                 })
                 .ToSitesResponsePagedList()
                 .ToPagedList(filters.Page, filters.Limit);

        }

        public async Task<List<SitesResponse>> GetList(Guid objId)
        {
            var _user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == objId).FirstOrDefaultAsync();

            if (_user != null)
            {
                var settingTimeZone = await _settingsServices.GetTimeZoneSetting(_user.Id);
                if (settingTimeZone != null) timeZone = settingTimeZone.ToString();
            }

            var sites = _unitOfWork._Sites.GetDbSet()
                .AsNoTracking()
                .Include(f => f.StudyCountry)
                .Include(f => f.ServiceType)
                .Where(f => f.Status == true)
                .Select(e => new Sites
                {
                    Id = e.Id,
                    ServiceType = e.ServiceType,
                    ServiceTypeId = e.ServiceTypeId,
                    StudyCountry = e.StudyCountry,
                    StudyCountryId = e.StudyCountryId,
                    SiteStatus = e.SiteStatus,
                    Status = e.Status,
                    Code = e.Code,
                    Name = e.Name,
                    StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.StartDate, timeZone),
                    EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.EndDate, timeZone),
                    SitePhases = e.SitePhases
                    .Select(f => new SitePhases
                    {
                        Id = f.Id,
                        EndDate = f.EndDate,
                        SitesId = f.SitesId,
                        StartDate = f.StartDate,
                        Status = f.Status,
                        PhaseId = f.PhaseId,
                    }).ToList()
                })
                .ToList();
            return sites.ToSiteResponseList();
        }

        public async Task<SitesResponse?> Save(Guid objId, SitesRequest req)
        {
            Sites entity = new Sites
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Code = req.Code,
                SiteStatus = req.SiteStatus,
                StartDate = req.StartDate.ToUniversalTime(),
                EndDate = req.EndDate.ToUniversalTime(),
                Status = req.Status,
                StudyCountryId = req.StudyCountryId,
                ServiceTypeId = req.ServiceTypeId,
                SitePhases = req.SitePhases
                    .Select(e => new SitePhases
                    {
                        PhaseId = e.PhaseId,
                        StartDate = e.StartDate.ToUniversalTime(),
                        EndDate = e.EndDate.ToUniversalTime(),
                        Status = e.Status,
                    }).ToList()
            };

            await _unitOfWork._Sites.Add(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToSitesResponse(); else return null;
        }

        public async Task<SitesResponse?> Update(Guid objId, SitesRequest req)
        {
            if (req == null) return null;

            var entity = await _unitOfWork._Sites.GetDbSet()
                .Include(f => f.SitePhases.Where(f => f.Status == true))
                .Where(f => f.Id == req.Id)
                .FirstOrDefaultAsync();

            if (entity is null) return null;

            entity.SiteStatus = req.SiteStatus;
            entity.Status = req.Status;
            entity.StartDate = req.StartDate.ToUniversalTime();
            entity.EndDate = req.EndDate.ToUniversalTime();
            entity.Code = req.Code;
            entity.Name = req.Name;
            entity.StudyCountryId = req.StudyCountryId;
            entity.ServiceTypeId = req.ServiceTypeId;

            if (req.SitePhases is not null)
            {
                foreach (var e in req.SitePhases)
                {
                    if (e.Id == Guid.Empty)
                    {
                        entity.SitePhases.Add(new SitePhases
                        {
                            PhaseId = e.PhaseId,
                            StartDate = e.StartDate.ToUniversalTime(),
                            EndDate = e.EndDate.ToUniversalTime(),
                            Status = e.Status
                        });
                    }
                    else
                    {
                        var existingEntity = entity.SitePhases.FirstOrDefault(f => f.Id == e.Id);
                        if (existingEntity != null)
                        {
                            existingEntity.PhaseId = e.PhaseId;
                            existingEntity.StartDate = e.StartDate.ToUniversalTime();
                            existingEntity.EndDate = e.EndDate.ToUniversalTime();
                            existingEntity.Status = e.Status;
                        }
                    }
                }
            }

            await _unitOfWork._Sites.Update(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToSitesResponse(); else return null;
        }

        public async Task<bool> Deactivate(Guid objectId, DeactivateRequest req)
        {
            if (req.Id == Guid.Empty) return false;
            var entity = await _unitOfWork._Sites.GetById(req.Id);
            if (entity is null) return false;
            entity.Status = false;

            if (entity.SitePhases != null && entity.SitePhases.Any())
            {
                foreach (var e in entity.SitePhases)
                {
                    e.Status = false;
                }
            }

            await _unitOfWork._Sites.Deactivate(entity);

            var res = await _unitOfWork.SaveData(objectId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return true; else return false;
        }

        public async Task<bool> DeactivateSitePhases(Guid objectId, DeactivateRequest req)
        {
            if (req.Id == Guid.Empty) return false;

            var entity = await _unitOfWork._SitePhases.GetById(req.Id);
            if (entity is null) return false;

            entity.Status = false;
            await _unitOfWork._SitePhases.Deactivate(entity);

            var res = await _unitOfWork.SaveData(objectId, isDelete: true, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return true; else return false;
        }

        public async Task<SitePhasesResponseDto?> GetSitePhasesById(Guid id, Guid objId)
        {
            if (id == Guid.Empty) return null;

            var _user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == objId).FirstOrDefaultAsync();

            var entity = await _unitOfWork._SitePhases.GetDbSet()
                .AsNoTracking()
                .Include(f => f.Phase)
                .Where(f => f.Id == id)
                .Select(e => new SitePhasesResponseDto
                {
                    Id = e.Id,
                    Phase = e.Phase,
                    PhaseId = e.PhaseId,
                    SitesId = e.SitesId,
                    StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.StartDate, timeZone),
                    EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.EndDate, timeZone),
                    Status = e.Status,
                }).FirstOrDefaultAsync();

            if (entity.Id == Guid.Empty) return null;

            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "SitePhases"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);

            return entity;
        }

        public async Task<SitePhasesResponseDto?> SaveSitePhases(SitePhasesRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = new SitePhases
            {
                SitesId = req.SitesId,
                StartDate = req.StartDate.ToUniversalTime(),
                EndDate = req.EndDate.ToUniversalTime(),
                Status = req.Status,
                PhaseId = req.PhaseId,
            };

            await _unitOfWork._SitePhases.Add(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Remarks ?? "");

            if (res > 0)
            {
                return new SitePhasesResponseDto
                {
                    Id = entity.Id,
                    SitesId = entity.SitesId,
                    EndDate = entity.EndDate.ToUniversalTime(),
                    StartDate = entity.StartDate.ToUniversalTime(),
                    Status = entity.Status,
                    PhaseId = entity.PhaseId,
                };
            }
            else return null;
        }

        public async Task<SitePhasesResponseDto?> UpdateSitePhases(SitePhasesRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = await _unitOfWork._SitePhases.GetById(req.Id);
            if (entity == null) return null;

            entity.SitesId = req.SitesId;
            entity.PhaseId = req.PhaseId;
            entity.StartDate = req.StartDate;
            entity.EndDate = req.EndDate;
            entity.Status = req.Status;

            await _unitOfWork._SitePhases.Update(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Remarks ?? "");

            if (res > 0)
            {
                return new SitePhasesResponseDto
                {
                    Id = entity.Id,
                    SitesId = entity.SitesId,
                    EndDate = entity.EndDate.ToUniversalTime(),
                    StartDate = entity.StartDate.ToUniversalTime(),
                    Status = entity.Status,
                    PhaseId = entity.PhaseId,
                };
            }
            else return null;
        }

        public async Task<bool> IsExist(string param)
        {

            var result = await _unitOfWork._Sites.GetDbSet()
                .AsNoTracking()
                .Where(f => f.Code.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Name.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return result.Any();
        }
    }
}
