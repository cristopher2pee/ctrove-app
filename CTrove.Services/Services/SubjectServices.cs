using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using CTrove.Core.Enum;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Extensions;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Services
{
    public class SubjectServices : ISubjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        public SubjectServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;

        }
        public async Task<SubjectResponse?> Save(SubjectRequest req, Guid objId)
        {
            if (req is null) return null;

            Subject subject = new Subject
            {
                Id = Guid.NewGuid(),
                Status = req.Status,
                RandNo = req.RandNo,
                ScreeningNo = req.ScreeningNo,
                EthnicityId = req.EthnicityId,
                RaceId = req.RaceId,
                Sex = req.Sex,
                SitesId = req.SitesId,
                SubjectStatus = req.SubjectStatus,
                YearOfBirth = req.YearOfBirth,
                SubjectPhases = req.SubjectPhases
                    .Select(e => new SubjectPhases
                    {
                        PhaseId = e.PhaseId,
                        StartDate = e.StartDate.ToUniversalTime(),
                        EndDate = e.EndDate.ToUniversalTime(),
                        Status = e.Status,
                    }).ToList()
            };

            await _unitOfWork._Subject.Add(subject);

            int result = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (result > 0) return subject.ToSubjectResponse(); else return null;

        }
        public async Task<SubjectResponse?> Update(SubjectRequest req, Guid objId)
        {
            if (req is null) return null;

            var entity = await _unitOfWork._Subject.GetDbSet()
                .Include(f => f.SubjectPhases.Where(f => f.Status == true))
                .Where(f => f.Id == req.Id)
                .FirstOrDefaultAsync();

            if (entity is null) return null;

            entity.Status = req.Status;
            entity.RandNo = req.RandNo;
            entity.ScreeningNo = req.ScreeningNo;
            entity.EthnicityId = req.EthnicityId;
            entity.RaceId = req.RaceId;
            entity.Sex = req.Sex;
            entity.SitesId = req.SitesId;
            entity.SubjectStatus = req.SubjectStatus;
            entity.YearOfBirth = req.YearOfBirth;

            if (req.SubjectPhases is not null)
            {
                foreach (var e in req.SubjectPhases)
                {
                    if (e.Id == Guid.Empty)
                    {
                        entity.SubjectPhases.Add(new SubjectPhases
                        {
                            PhaseId = e.PhaseId,
                            StartDate = e.StartDate.ToUniversalTime(),
                            EndDate = e.EndDate.ToUniversalTime(),
                            Status = e.Status,
                        });
                    }
                    else
                    {
                        var existingEntity = entity.SubjectPhases.FirstOrDefault(f => f.Id == e.Id);
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
            await _unitOfWork._Subject.Update(entity);
            int result = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (result > 0) return entity.ToSubjectResponse(); else return null;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid userId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._Subject.GetDbSet()
                .Include(f => f.SubjectPhases)
                .Where(f => f.Id == req.Id)
                .FirstOrDefaultAsync();

            if (entity == null) return false;
            entity.Status = false;

            if (entity.SubjectPhases != null && entity.SubjectPhases.Any())
            {
                foreach (var e in entity.SubjectPhases)
                {
                    e.Status = false;
                }
            }

            await _unitOfWork._Subject.Deactivate(entity);
            if (await _unitOfWork.SaveData(userId, isDelete: true, remarks: req.Remarks ?? "",
                 location: req.Location ?? "") > 0) return true;
            else return false;
        }

        public async Task<bool> DeactivateSubjectPhase(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._SubjectPhases.GetDbSet()
                .Where(f => f.Id == req.Id)
                .FirstOrDefaultAsync();

            if (entity == null) return false;
            entity.Status = false;

            await _unitOfWork._SubjectPhases.Deactivate(entity);
            if (await _unitOfWork.SaveData(objId, isDelete: true, remarks: req.Remarks ?? "",
                 location: req.Location ?? "") > 0) return true;
            else return false;
        }

        public async Task<SubjectResponse?> GetById(Guid id, Guid objId)
        {
            var result = await _unitOfWork._Subject.GetDbSet()
                .AsNoTracking()
                .Include(f => f.Ethnicity)
                .Include(f => f.Race)
                .Include(f => f.Sites)
                .ThenInclude(f => f.StudyCountry)
                .Include(f => f.SubjectPhases.Where(f => f.Status == true))
                .ThenInclude(f => f.Phase)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (result is null) return null;

            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = result,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = result.Id,
                Table = "Subject"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);

            return result.ToSubjectResponse();
        }

        public async Task<PagedResult<SubjectResponse>> GetAll(SubjectFilters filters)
        {

            return _unitOfWork._Subject.GetDbSet()
                .AsNoTracking()
                .AsQueryable()
                .Include(f => f.Ethnicity)
                .Include(f => f.Race)
                .Include(f => f.Sites).ThenInclude(f => f.StudyCountry)
                .Include(f => f.SubjectPhases.Where(f => f.Status == true))
                .Where(f =>
                    (
                       f.ScreeningNo.Contains(filters.Search)
                       || f.RandNo.Contains(filters.Search)
                       || f.Ethnicity.Code.Contains(filters.Search)
                       || f.Ethnicity.Name.Contains(filters.Search)
                       || f.Race.Code.Contains(filters.Search)
                       || f.Race.Name.Contains(filters.Search)
                       || f.Sites.Code.Contains(filters.Search)
                       || f.Sites.Name.Contains(filters.Search)
                       || f.Sites.StudyCountry.Code.Contains(filters.Search)
                       || f.Sites.StudyCountry.Name.Contains(filters.Search)
                    )
                    && f.Status == filters.Status
                    && (filters.SubjectStatus != null ? filters.SubjectStatus.Contains(f.SubjectStatus) : true)
                    && (filters.EthnicityIds != null ? filters.EthnicityIds.Contains(f.EthnicityId) : true)
                    && (filters.RaceIds != null ? filters.RaceIds.Contains(f.RaceId) : true)
                    && (filters.SitesIds != null ? filters.SitesIds.Contains(f.SitesId) : true)
                    && (filters.StudyCountryIds != null ? filters.StudyCountryIds.Contains(f.Sites.StudyCountryId) : true)
                    )
                .ToSubjectResponseList()
                .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<SubjectPhasesResponse?> AddSubjectPhase(SubjectPhasesRequest req, Guid objId)
        {
            if (req is null) return null;
            var entity = new SubjectPhases
            {
                SubjectId = req.SubjectId,
                StartDate = req.StartDate.ToUniversalTime(),
                EndDate = req.EndDate.ToUniversalTime(),
                Status = req.Status,
                PhaseId = req.PhaseId,
            };

            await _unitOfWork._SubjectPhases.Add(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToSubjectPhasesResponse(); else return null;

        }

        public async Task<SubjectPhasesResponse?> UpdateSubjectPhase(SubjectPhasesRequest req, Guid objId)
        {
            if (req is null) return null;
            var entity = await _unitOfWork._SubjectPhases.GetById(req.Id);
            if (entity == null) return null;

            entity.SubjectId = req.SubjectId;
            entity.PhaseId = req.PhaseId;
            entity.Status = req.Status;
            entity.EndDate = req.EndDate.ToUniversalTime();
            entity.StartDate = req.StartDate.ToUniversalTime();

            await _unitOfWork._SubjectPhases.Update(entity);
            int res = await _unitOfWork.SaveData(objId, remarks: req.Remarks ?? "", location: req.Location ?? "");
            if (res > 0) return entity.ToSubjectPhasesResponse(); else return null;
        }

        public async Task<SubjectPhasesResponse?> GetSubjectPhasebyId(Guid id, Guid objId)
        {
            if (id == Guid.Empty) return null;

            var entity = await _unitOfWork._SubjectPhases.GetDbSet()
                .Include(f => f.Phase)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null) return null;
            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "SubjectPhases"
            };
            await _auditTrailServices.PerformAuditTrailforRetrieved(req);

            return entity.ToSubjectPhasesResponse();
        }
    }
}
