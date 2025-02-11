using Ctrove.HR.Services;
using CTrove.Core.Common;
using CTrove.Core.DTO;
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
    public interface IContributorStudyServices
    {
        Task<PagedResult<ContributorStudyResponse>> GetPagedList(ContributorStudyFilters req);
        Task<IEnumerable<ContributorStudyResponse>> GetList();
        Task<ContributorStudyResponse?> Get(Guid id, Guid objId);
        Task<ContributorStudyResponse?> Add(ContributorStudyRequest req, Guid objId);
        Task<ContributorStudyResponse?> Update(ContributorStudyRequest req, Guid objId);
    }
    public class ContributorStudyServices : IContributorStudyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrContributorStudyServices _hrContributorStudyServices;

        public ContributorStudyServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices, IHrContributorStudyServices hrContributorStudyServices)
        {
            _auditTrailServices = auditTrailServices;
            _unitOfWork = unitOfWork;
            _hrContributorStudyServices = hrContributorStudyServices;
        }

        public async Task<PagedResult<ContributorStudyResponse>> GetPagedList(ContributorStudyFilters filter)
        {
            var entities = _unitOfWork._ContributorStudy
                .GetDbSet()
                .AsEnumerable()
                .Where(f => (f.StudyName.Contains(filter.Search, StringComparison.OrdinalIgnoreCase))
                    && (filter.ListStudyIds != null && filter.ListStudyIds.Any() ?
                        filter.ListStudyIds.Where(e => e.Equals(f.StudyId)).Any() : true))
                .OrderBy(f => f.StudyName)
                .ToContributorStudyResponseList();
               
                return entities.ToPagedList(filter.Page, filter.Limit);

        }


        public async Task<IEnumerable<ContributorStudyResponse>> GetList()
            => _unitOfWork._ContributorStudy.GetDbSet()
            .AsNoTracking()
            .ToContributorStudyResponseList()
            .ToList();

        public async Task<ContributorStudyResponse?> Get(Guid id, Guid objId)
        {
            var result = await _unitOfWork._ContributorStudy.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == id);
            if (result == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(
                new Core.DTO.Response.AuditRetrievedRequest
                {
                    obj = result,
                    AuditType = Core.Enum.AuditType.View,
                    PerformedBy = objId,
                    recordId = result.Id,
                    Table = "ContributorStudy"
                });

            return result.ToContributorStudyResponse();
        }
        public async Task<ContributorStudyResponse?> Add(ContributorStudyRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = new ContributorStudy
            {
                Id = Guid.NewGuid(),
                StudyId = req.StudyId,
                StudyName = req.StudyName,
                ContributorId = req.ContributorId,
                SponsorId = req.SponsorId,
                StartDate = req.StartDate != null ? req.StartDate.Value.ToUniversalTime() : null,
                EndDate = req.EndDate != null ? req.EndDate.Value.ToUniversalTime() : null,
                Status = req.Status,
                Role = req.Role
            };

            await _unitOfWork._ContributorStudy.Add(entity);
            int res = await _unitOfWork.SaveData(
                userId: objId,
                remarks: req.Remarks ?? "",
                location: req.Location ?? ""
                );

            await _hrContributorStudyServices.Add(entity.ConvertToHrContributorStudy(new Core.DTO.HR.HrBaseRequest
            {
                UserObjectId = objId,
                Location = req.Location,
                Remarks = req.Remarks
            }));

            return res > 0 ? entity.ToContributorStudyResponse() : null;
        }
        public async Task<ContributorStudyResponse?> Update(ContributorStudyRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = await _unitOfWork._ContributorStudy.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == req.Id);
            if (entity == null) return null;

            entity.StudyId = req.StudyId;
            entity.StudyName = req.StudyName;
            entity.ContributorId = req.ContributorId;
            entity.SponsorId = req.SponsorId;
            entity.StartDate = req.StartDate != null ? req.StartDate.Value.ToUniversalTime() : null;
            entity.EndDate = req.EndDate != null ? req.EndDate.Value.ToUniversalTime() : null;
            entity.Status = req.Status;
            entity.Role = req.Role;

            await _unitOfWork._ContributorStudy.Update(entity);
            int res = await _unitOfWork.SaveData(
                userId: objId,
                remarks: req.Remarks ?? "",
                location: req.Location ?? ""
                );

            return res > 0 ? entity.ToContributorStudyResponse() : null;
        }
    }
}
