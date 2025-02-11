using Ctrove.HR.Model;
using Ctrove.HR.Services;
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
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace CTrove.Services.Services
{
    public interface IOrganizationServices
    {
        Task<PagedResult<OrganizationResponse>> GetPageList(OrganizationFilter filters);
        Task<IEnumerable<OrganizationResponse>> GetList();
        Task<OrganizationResponse?> Add(OrganizationRequest req, Guid objId);
        Task<OrganizationResponse?> Update(OrganizationRequest req, Guid objId);
        Task<bool> Deactivate(DeactivateRequest req, Guid objId);
        Task<OrganizationResponse?> Get(Guid id, Guid objId);
        Task<bool> isOrganizationExist(string param);
    }
    public class OrganizationServices : IOrganizationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailServices _auditTrailServices;
        private readonly IHrOrganizationServices _hrOrganizationServices;
        public OrganizationServices(IUnitOfWork unitOfWork, IAuditTrailServices auditTrailServices
            , IHrOrganizationServices hrOrganizationServices)
        {
            _unitOfWork = unitOfWork;
            _auditTrailServices = auditTrailServices;
            _hrOrganizationServices = hrOrganizationServices;
        }

        public async Task<bool> isOrganizationExist(string param)
        {

            var entity = await _unitOfWork._Organization.GetDbSet()
                .Where(f => f.CompanyName.ToUpper().Trim() == param.ToUpper().Trim()
                || f.Email.ToUpper().Trim() == param.ToUpper().Trim())
                .ToListAsync();

            return entity.Any();
        }

        public async Task<OrganizationResponse?> Get(Guid id, Guid objId)
        {
            var item = await _unitOfWork._Organization.GetDbSet().FirstOrDefaultAsync(f => f.Id == id);
            if (item == null) return null;

            await _auditTrailServices.PerformAuditTrailforRetrieved(
                new Core.DTO.Response.AuditRetrievedRequest
                {
                    obj = item,
                    AuditType = Core.Enum.AuditType.View,
                    PerformedBy = objId,
                    recordId = item.Id,
                    Table = "Organization"
                });

            return new OrganizationResponse
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Country = _unitOfWork._Country.GetDbSet().FirstOrDefault(f => f.Id == item.CountryId),
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                ContactType = item.ContactType,
                VendorTypeId = item.VendorTypeId,
                VendorType = item.VendorType,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                PrimaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.PrimaryContactId),
                SecondaryContactId = item.SecondaryContactId,
                SecondaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.SecondaryContactId),
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
            };



        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            if (req == null) return false;

            var entity = await _unitOfWork._Organization.GetDbSet().FirstOrDefaultAsync(f => f.Id == req.Id);
            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._Organization.Deactivate(entity);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? "",
                    isDelete: true
                );

            return result > 0 ? true : false;

        }
        public async Task<OrganizationResponse?> Update(OrganizationRequest req, Guid objId)
        {
            if (req == null) return null;
            var item = await _unitOfWork._Organization.GetDbSet()
                .FirstOrDefaultAsync(f => f.Id == req.Id);

            if (item == null) return null;

            //item = req.ConvertOrganizationRequestToEntity(false);

            item.CountryId = req.CountryId;
            item.CompanyName = req.CompanyName;
            item.Parent = req.Parent;
            item.ContactTypeId = req.ContactTypeId;
            item.VendorTypeId = req.VendorTypeId;
            item.AddressLine1 = req.AddressLine1;
            item.AddressLine2 = req.AddressLine2;
            item.AddressLine3 = req.AddressLine3;
            item.ZipCode = req.ZipCode;
            item.City = req.City;
            item.State = req.State;
            item.PhoneNumber = req.PhoneNumber;
            item.FaxNumber = req.FaxNumber;
            item.Website = req.Website;
            item.PrimaryContactId = req.PrimaryContactId;
            item.SecondaryContactId = req.SecondaryContactId;
            item.Email = req.Email;
            item.Notes = req.Notes;
            item.Status = req.Status;

            await _unitOfWork._Organization.Update(item);

            int result = await _unitOfWork.SaveData(
                    userId: objId,
                    remarks: req.Remarks ?? "",
                    location: req.Location ?? ""
                    );

            return result > 0 ? new OrganizationResponse
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Country = _unitOfWork._Country.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.CountryId),
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                ContactType = item.ContactType,
                VendorTypeId = item.VendorTypeId,
                VendorType = item.VendorType,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                PrimaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.PrimaryContactId),
                SecondaryContactId = item.SecondaryContactId,
                SecondaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.SecondaryContactId),
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
            } : null;
        }

        public async Task<OrganizationResponse?> Add(OrganizationRequest req, Guid objId)
        {
            if (req == null) return null;

            var item = req.ConvertOrganizationRequestToEntity(true);

            await _unitOfWork._Organization.Add(item);
            await _hrOrganizationServices.Add(item.ConvertToHrOrganizationRequest(new Core.DTO.HR.HrBaseRequest
            {
                UserObjectId = objId,
                Location = req.Location,
                Remarks = req.Remarks
            }));

            int result = await _unitOfWork.SaveData(
                userId: objId,
                remarks: req.Remarks ?? "",
                location: req.Location ?? ""
                );

            return result > 0 ? new OrganizationResponse
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Country = _unitOfWork._Country.GetDbSet().FirstOrDefault(f => f.Id == item.CountryId),
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                ContactType = item.ContactType,
                VendorTypeId = item.VendorTypeId,
                VendorType = item.VendorType,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                PrimaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.PrimaryContactId),
                SecondaryContactId = item.SecondaryContactId,
                SecondaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.SecondaryContactId),
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
            } : null;

        }
        public async Task<IEnumerable<OrganizationResponse>> GetList()
        {
            var entities = await _unitOfWork._Organization.GetDbSet()
                .AsQueryable()
                .OrderBy(f => f.CompanyName)
                .ToListAsync();

            return entities.Select(item => new OrganizationResponse
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Country = _unitOfWork._Country.GetDbSet().FirstOrDefault(f => f.Id == item.CountryId),
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                ContactType = item.ContactType,
                VendorTypeId = item.VendorTypeId,
                VendorType = item.VendorType,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                PrimaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.PrimaryContactId),
                SecondaryContactId = item.SecondaryContactId,
                SecondaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.SecondaryContactId),
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
            }).ToList();
        }
        public async Task<PagedResult<OrganizationResponse>> GetPageList(OrganizationFilter filters)
        {
            List<OrganizationResponse> listOrgResponse = new List<OrganizationResponse>();
            var listEntity = await _unitOfWork._Organization.GetDbSet()
                .AsNoTracking()
                .Include(f => f.ContactType)
                .Include(f => f.VendorType)
                .ToListAsync();

            listOrgResponse = listEntity.Select(item => new OrganizationResponse
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Country = _unitOfWork._Country.GetDbSet().FirstOrDefault(f => f.Id == item.CountryId),
                CompanyName = item.CompanyName,
                Parent = item.Parent,
                ContactTypeId = item.ContactTypeId,
                ContactType = item.ContactType,
                VendorTypeId = item.VendorTypeId,
                VendorType = item.VendorType,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ZipCode = item.ZipCode,
                City = item.City,
                State = item.State,
                PhoneNumber = item.PhoneNumber,
                FaxNumber = item.FaxNumber,
                Website = item.Website,
                PrimaryContactId = item.PrimaryContactId,
                PrimaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.PrimaryContactId),
                SecondaryContactId = item.SecondaryContactId,
                SecondaryContributorContact = _unitOfWork._Contributor.GetDbSet()
                    .FirstOrDefault(f => f.Id == item.SecondaryContactId),
                Email = item.Email,
                Notes = item.Notes,
                Status = item.Status,
            }).ToList();

            return listOrgResponse.AsEnumerable()
                .Where(f => (f.CompanyName.Contains(filters.Search, StringComparison.OrdinalIgnoreCase)
                    || (f.PrimaryContributorContact != null ? f.PrimaryContributorContact.Firstname
                        .Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false)
                    || (f.PrimaryContributorContact != null ? f.PrimaryContributorContact.Lastname
                        .Contains(filters.Search, StringComparison.OrdinalIgnoreCase) : false))
                    && (filters.ListCountryId != null && filters.ListCountryId.Any() ?
                        filters.ListCountryId.Where(e => e.Equals(f.CountryId)).Any() : true)
                    && (filters.parentTypes != null && filters.parentTypes.Any() ?
                        filters.parentTypes.Where(e => e.Equals(f.Parent)).Any() : true)
                    && f.Status == filters.Status
                )
                .ToPagedList(filters.Page, filters.Limit);
        }
    }
}
