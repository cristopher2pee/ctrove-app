using Ctrove.HR.Model;
using Ctrove.HR.Services;
using CTrove.Core.Common;
using CTrove.Core.DTO;
using CTrove.Core.DTO.HR;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Extensions;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CTrove.Services.Services
{
    public interface IContributorServices
    {
        Task<PagedResult<ContributorResponse>> GetPagedList(ContributorFiltes filters);
        Task<IEnumerable<ContributorResponse>> GetList();
        Task<ContributorResponse?> Get(Guid id, Guid objId);
        Task<ContributorResponse?> Add(ContributorRequest req, Guid objId);
        Task<ContributorResponse?> Update(ContributorRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);

        Task<bool> isContributorNameExist(string firstname, string lastname);
        Task<bool> isContributorEmailExist(string email);

        Task<List<ContributorResponse>?> SearchContributor(string paramSearch);

    }
    public class ContributorServices : IContributorServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrContributorServices _hrContributorServices;
        public ContributorServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices,
            IHrContributorServices hrContributorServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
            _hrContributorServices = hrContributorServices;
        }

        public async Task<List<ContributorResponse>?> SearchContributor(string paramSearch)
        {
            if (string.IsNullOrEmpty(paramSearch)) return null;
            return _unitOfWork._Contributor.GetDbSet()
                .AsNoTracking()
                .Include(f => f.ContributorStudy)
                .Include(f => f.Country)
                .Include(f => f.Organization)
                .AsEnumerable()
                .Where(f =>
                     (
                     f.Email.Contains(paramSearch, StringComparison.OrdinalIgnoreCase) ||
                      f.Lastname.Contains(paramSearch, StringComparison.OrdinalIgnoreCase) ||
                     f.Firstname.Contains(paramSearch, StringComparison.OrdinalIgnoreCase)
                     ))
                .ToContributorResponseList()
                .ToList();
        }

        public async Task<bool> isContributorEmailExist(string email)
        {
            var entity = await _unitOfWork._Contributor.GetDbSet()
                .Where(f => f.Email.ToUpper().Trim() == email.ToUpper().Trim())
                .ToListAsync();

            return entity.Any();
        }

        public async Task<bool> isContributorNameExist(string firstname, string lastname)
        {
            var entity = await _unitOfWork._Contributor.GetDbSet()
                .Where(f => f.Firstname.ToUpper().Trim() == firstname.ToUpper().Trim()
                && f.Lastname.ToUpper().Trim() == lastname.ToUpper().Trim())
                .ToListAsync();

            return entity.Any();
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._Contributor.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == req.Id);

            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._Contributor.Deactivate(entity);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? "",
                    isDelete: true
                );

            return result > 0 ? true : false;
        }
        public async Task<ContributorResponse?> Update(ContributorRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = await _unitOfWork._Contributor.GetDbSet()
                .FirstOrDefaultAsync(x => x.Id == req.Id);

            if (entity == null) return null;

           // entity.Id = Guid.NewGuid();
            entity.ObjectId = req.ObjectId;
            entity.CountryId = req.CountryId;
            entity.Email = req.Email;
            entity.Prefix = req.Prefix;
            entity.Firstname = req.Firstname;
            entity.Lastname = req.Lastname;
            entity.Grade = req.Grade;
            entity.PrimaryJobTitle = req.PrimaryJobTitle;
            entity.SecondaryJobTitle = req.SecondaryJobTitle;
            entity.Phone = req.Phone;
            entity.Mobile = req.Mobile;
            entity.PublicData = req.PublicData;
            entity.OrganizationId = req.OrganizationId;
            entity.City = req.City;
            entity.Consent = req.Consent;
            entity.Status = req.Status;
            entity.DateOfConsent = req.DateOfConsent != null ? req.DateOfConsent.Value.ToUniversalTime() : null;

            await _unitOfWork._Contributor.Update(entity);

            int res = await _unitOfWork.SaveData(
                userId: objId,
                remarks: req.Remarks ?? "",
                location: req.Location ?? ""
                );

            return res > 0 ? entity.ToContributorResponse() : null;

        }
        public async Task<ContributorResponse?> Add(ContributorRequest req, Guid objId)
        {
            if (req == null) return null;
            var entity = new Contributor
            {
                Id = Guid.NewGuid(),
                ObjectId = objId,
                CountryId = req.CountryId,
                Email = req.Email,
                Prefix = req.Prefix,
                Firstname = req.Firstname,
                Lastname = req.Lastname,
                Grade = req.Grade,
                PrimaryJobTitle = req.PrimaryJobTitle,
                SecondaryJobTitle = req.SecondaryJobTitle,
                Phone = req.Phone,
                Mobile = req.Mobile,
                PublicData = req.PublicData,
                OrganizationId = req.OrganizationId,
                City = req.City,
                Consent = req.Consent,
                Status = req.Status,
                DateOfConsent = req.DateOfConsent != null ? req.DateOfConsent.Value.ToUniversalTime() : null
            };

            await _unitOfWork._Contributor.Add(entity);
            int res = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? ""
                    );

            await _hrContributorServices.Add(entity.ConvertToHrContributor(new HrBaseRequest
            {
                UserObjectId = objId,
                Location = req.Location,
                Remarks = req.Remarks
            }));

            return res > 0 ? entity.ToContributorResponse() : null;

        }
        public async Task<ContributorResponse?> Get(Guid id, Guid objId)
        {
            var result = await _unitOfWork._Contributor.GetDbSet()
                .Include(f => f.ContributorStudy)
                .Include(f => f.Country)
                .Include(f => f.Organization)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (result == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(
                new Core.DTO.Response.AuditRetrievedRequest
                {
                    obj = result,
                    AuditType = Core.Enum.AuditType.View,
                    PerformedBy = objId,
                    recordId = result.Id,
                    Table = "Contributor"
                });

            return result.ToContributorResponse();

        }
        public async Task<PagedResult<ContributorResponse>> GetPagedList(ContributorFiltes filters)
        {
            return _unitOfWork._Contributor.GetDbSet()
                .AsNoTracking()
                .AsQueryable()
                .Include(f => f.ContributorStudy)
                .Include(f => f.Country)
                .Include(f => f.Organization)
                .Where(f => (f.Email.Contains(filters.Search) || f.Lastname.Contains(filters.Search)
                    || f.Firstname.Contains(filters.Search))
                    && (filters.ListCountryId != null ? filters.ListCountryId.Contains(f.CountryId) : true)
                    && (filters.ListOrganizationId != null ? filters.ListOrganizationId.Contains(f.OrganizationId) : true)
                    )
                .OrderBy(f => f.Lastname)
                .ThenBy(f => f.Firstname)
                .ToContributorResponseList()
                .ToPagedList(filters.Page, filters.Limit);
        }

        public async Task<IEnumerable<ContributorResponse>> GetList()
        {
            return _unitOfWork._Contributor.GetDbSet()
                .AsNoTracking()
                .AsEnumerable()
                .ToContributorResponseList();
        }

    }
}
