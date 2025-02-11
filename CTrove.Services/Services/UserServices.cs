using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Interface;
using CTrove.Core.DTO.Response;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Services.Extensions;
using System.ComponentModel.DataAnnotations;
using CTrove.Core.Common;
using CTrove.Core.Filters;

namespace CTrove.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingsServices _settingsServices;
        private readonly ITimeZoneServices _timeZoneServices;
        private readonly IAuditTrailServices _auditTrailServices;

        private string timeZone = "Asia/Singapore";
        public UserServices(IUnitOfWork unitOfWork, ISettingsServices settingsServices, ITimeZoneServices timeZoneServices, IAuditTrailServices auditTrailServices)
        {
            _unitOfWork = unitOfWork;
            _settingsServices = settingsServices;
            _timeZoneServices = timeZoneServices;
            _auditTrailServices = auditTrailServices;
        }
        public async Task<UserResponse> isUserOnBoarding(Guid userId)
        {
            if (userId == Guid.Empty) return null;

            var user = await _unitOfWork._User.FindByCondition(f => f.ObjectId == userId);
            if (user == null) return null;

            return user.ToUserResponse();
        }
        public async Task<UserResponse> UserOnBoarding(Guid userId, UserRequest req)
        {
            var user = await _unitOfWork._User.GetDbSet()
               .Where(f => f.ObjectId == userId)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                user = await _unitOfWork._User
                   .GetDbSet()
                   .Where(f => f.Email.ToUpper().Trim() == req.Email.ToUpper().Trim())
                   .FirstOrDefaultAsync();

                if (user != null)
                {
                    user.ObjectId = userId;
                    user.Prefix = req.Prefix;
                    user.Firstname = req.Firstname;
                    user.Lastname = req.Lastname;
                    user.Middlename = req.Middlename;
                    user.Email = req.Email;
                    user.Suffix = req.Suffix;
                    user.Mobile = req.Mobile;
                    user.Landline = req.Landline;
                    user.StartDate = req.StartDate.ToUniversalTime();
                    user.EndDate = req.EndDate.ToUniversalTime();
                    user.Organization = req.Organization;
                    user.Status = req.Status;
                    user.RolesId = req.RolesId;
                    await _unitOfWork._User.Update(user);
                }
                int result = await _unitOfWork.SaveData(userId, location: req.Location ?? "", remarks: req.Remarks ?? "");
                if (result > 0)
                {
                    SettingsRequest settingReq = new SettingsRequest
                    {
                        UserId = user!.Id,
                        Remarks = req.Remarks ?? "",
                        TimeZone = req.Location ?? "",
                    };
                    await updateSettings(settingReq, userId);

                    return user.ToUserResponse();
                }
                else { return null; }
            }
            return user.ToUserResponse();
        }

        private async Task updateSettings(SettingsRequest req, Guid objId)
        {
            var settings = await _unitOfWork._Settings.GetDbSet().Where(f => f.UserId == req.UserId).FirstOrDefaultAsync();
            if (settings is null)
            {
                await _settingsServices.Save(req, objId);
            }
            else
            {
                req.Id = settings.Id;
                await _settingsServices.Update(req, objId);
            }
        }

        public async Task<UserResponse> GetProfile(Guid userId, string email)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(email)) return null;

            var user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == userId)
                .Include(f => f.Roles).FirstOrDefaultAsync();

            if (user == null)
            {
                user = await _unitOfWork._User.GetDbSet()
                    .Where(f => f.Email.ToUpper().Trim() == email.ToUpper().Trim())
                    .Include(f => f.Roles).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.ObjectId == null || user.ObjectId == Guid.Empty)
                    {
                        user.ObjectId = userId;
                        await _unitOfWork._User.Update(user);
                        int result = await _unitOfWork.SaveData(userId);
                        if (result > 0) return user.ToUserResponse(); else return null;
                    }
                }
                else return null;
            }
            return user.ToUserResponse();
        }

        public async Task<UserAccessResponse?> GetUserProfile(Guid objId, string email)
        {
            var user = await _unitOfWork._User.GetDbSet()
                .Where(f => f.ObjectId == objId)
                .Include(f => f.Roles)
                .ThenInclude(f => f.RolesPages)
                .FirstOrDefaultAsync();

            UserAccessResponse userAccessResponse = new UserAccessResponse();

            if (user == null)
            {
                user = await _unitOfWork._User.GetDbSet()
                     .Where(f => f.Email.ToUpper().Trim() == email.ToUpper().Trim())
                     .Include(f => f.Roles)
                     .ThenInclude(f => f.RolesPages)
                     .FirstOrDefaultAsync();

                if (user is null) return null;
            }
            userAccessResponse.UserResponse = user;
            // userAccessResponse.UserResponse = user.ToUserResponse();
            var _accessList = await _unitOfWork._Access.GetDbSet()
                .Where(f => f.UserId == user.Id).ToListAsync();

            if (_accessList is null) return null;

            foreach (var item in _accessList)
            {
                if (item is not null)
                {
                    item.User = null;
                    userAccessResponse.AccessListResponse.Add(item);

                    var sites = await _unitOfWork._Sites.GetDbSet()
                        .FirstOrDefaultAsync(f => f.Id == item.AccessLevelId);

                    if (sites is not null)
                        userAccessResponse.SitesListResponse.Add(sites);

                    var studyCountry = await _unitOfWork._StudyCountry.GetDbSet()
                        .FirstOrDefaultAsync(f => f.Id == item.AccessLevelId);

                    if (studyCountry is not null)
                        userAccessResponse.StudyCountryListResponse.Add(studyCountry);
                }


            };

            return userAccessResponse is null ? null : userAccessResponse;
        }

        public async Task<UserResponse?> Update(Guid userId, UserRequest req)
        {
            if (userId == null || req == null) return null;

            var entityUser = await _unitOfWork._User.GetById(req.Id);
            if (entityUser == null) return null;

            entityUser.Prefix = req.Prefix;
            entityUser.Firstname = req.Firstname;
            entityUser.Lastname = req.Lastname;
            entityUser.Middlename = req.Middlename;
            entityUser.Email = req.Email;
            entityUser.Suffix = req.Suffix;
            entityUser.Mobile = req.Mobile;
            entityUser.Landline = req.Landline;
            entityUser.StartDate = req.StartDate.ToUniversalTime();
            entityUser.EndDate = req.EndDate.ToUniversalTime();
            entityUser.Organization = req.Organization;
            entityUser.RolesId = req.RolesId;
            entityUser.Status = req.Status;

            await _unitOfWork._User.Update(entityUser);
            var result = await _unitOfWork.SaveData(userId, location: req.Location ?? "", remarks: req.Remarks ?? "");
            if (result > 0) return entityUser.ToUserResponse(); else return null;

        }

        public async Task<bool> Delete(Guid userId)
        {
            if (userId == null && userId == Guid.Empty) return false;

            var entity = await _unitOfWork._User.GetById(userId);
            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._User.Deactivate(entity);
            if (await _unitOfWork.SaveData(userId, isDelete: true) > 0) return true; else return false;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objctId)
        {
            if (req == null && req.Id == Guid.Empty) return false;

            var entity = await _unitOfWork._User.GetById(req.Id);
            if (entity == null) return false;

            entity.Status = false;
            await _unitOfWork._User.Deactivate(entity);
            if (await _unitOfWork.SaveData(objctId, isDelete: true, location: req.Location ?? "", remarks: req.Remarks ?? "") > 0) return true; else return false;
        }

        public async Task<UserResponse?> Add(Guid objctId, UserRequest req)
        {
            if ((objctId == Guid.Empty) || req is null)
                return null;

            var entity = new User
            {
                Id = Guid.NewGuid(),
                ObjectId = Guid.Empty,
                Prefix = req.Prefix,
                Firstname = req.Firstname,
                Lastname = req.Lastname,
                Middlename = req.Middlename,
                Suffix = req.Suffix,
                Email = req.Email,
                Mobile = req.Mobile,
                Landline = req.Landline,
                StartDate = req.StartDate.ToUniversalTime(),
                EndDate = req.EndDate.ToUniversalTime(),
                Organization = req.Organization,
                RolesId = req.RolesId,
                Status = req.Status,
            };

            await _unitOfWork._User.Add(entity);
            var result = await _unitOfWork.SaveData(objctId, location: req.Location ?? "", remarks: req.Remarks ?? "");
            if (result > 0) return entity.ToUserResponse(); else return null;
        }

        public async Task<PagedResult<UserResponse>> GetAll(UserFilters filters)
            => _unitOfWork._User.GetDbSet()
                    .AsNoTracking()
                    .AsQueryable()
                    .Include(f => f.Roles)
                    .Where(f =>
                        (
                            f.Lastname.Contains(filters.Search)
                            || f.Firstname.Contains(filters.Search)
                            || f.Organization.Contains(filters.Search)
                            || f.Email.Contains(filters.Search)
                        )
                        && f.Status == filters.Status
                        && (filters.RolesId != null ? filters.RolesId.Contains(f.RolesId) : true)
                    )
                    .OrderBy(f => f.Lastname)
                    .ThenBy(f => f.Firstname)
                    .ToList()
                    .ToUserResponseList()
                    .ToPagedList(filters.Page, filters.Limit);


        public async Task<bool> IsUserEmailExist(string email)
        {
            var entity = await _unitOfWork._User.GetDbSet()
                .Where(f => f.Email.ToUpper().Trim() == email.ToUpper().Trim())
                .FirstOrDefaultAsync();

            if (entity != null) return true; else return false;
        }

        public async Task<UserResponse> GetById(Guid objId, Guid id)
        {
            if (id == Guid.Empty || id == null) return null;

            var settings = await _unitOfWork._Settings.GetDbSet()
                .Where(f => f.UserId == id).FirstOrDefaultAsync();

            if (settings != null) timeZone = settings.TimeZone;

            var entity = await _unitOfWork._User.GetDbSet()
                .AsNoTracking()
                .Include(f => f.Roles)
                .Where(f => f.Id == id)
                .Select(e => new User
                {
                    Id = e.Id,
                    ObjectId = e.ObjectId,
                    Firstname = e.Firstname,
                    Lastname = e.Lastname,
                    Middlename = e.Middlename,
                    Prefix = e.Prefix,
                    Suffix = e.Suffix,
                    Email = e.Email,
                    Mobile = e.Mobile,
                    Landline = e.Landline,
                    StartDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.StartDate, timeZone),
                    EndDate = _timeZoneServices.ConvertToUserTimeZoneSettings(e.EndDate, timeZone),
                    Organization = e.Organization,
                    Roles = e.Roles,
                    RolesId = e.RolesId,
                    Status = e.Status,
                })
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            //  await _unitOfWork.auditTrailRetrieved(objId);
            AuditRetrievedRequest req = new AuditRetrievedRequest
            {
                obj = entity,
                AuditType = Core.Enum.AuditType.View,
                PerformedBy = objId,
                recordId = entity.Id,
                Table = "User"
            };

            await _auditTrailServices.PerformAuditTrailforRetrieved(req);
            return entity.ToUserResponse();

        }

        public async Task<UserResponse> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;
            var entity = await _unitOfWork._User.GetDbSet()
              .Include(f => f.Roles)
              .Where(f => f.Email.ToUpper().Trim() == email.ToUpper().Trim())
              .FirstOrDefaultAsync();

            if (entity == null) return null;
            return entity.ToUserResponse();
        }

        public async Task<UserResponse> InviteUserSave(AccountsRequest req, Guid objectId)
        {
            var entity = await _unitOfWork._User.GetDbSet()
                .Where(f => f.Email.ToUpper().Trim() == req.email.ToUpper().Trim()).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Email = req.email;
                entity.RolesId = req.rolesId;

                await _unitOfWork._User.Update(entity);
                var res = await _unitOfWork.SaveData(objectId);
                if (res > 0) return entity.ToUserResponse(); else return null;
            };

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = req.email,
                RolesId = req.rolesId,
                Firstname = string.Empty,
                Lastname = string.Empty,
                Middlename = string.Empty,
                Mobile = string.Empty,
                Organization = string.Empty,
                Status = true
            };

            await _unitOfWork._User.Add(newUser);
            var result = await _unitOfWork.SaveData(objectId);
            if (result > 0) return newUser.ToUserResponse(); else return null;
        }

        public async Task<List<AccessListResponse>> UserAccessRights(Guid userId)
        {
            List<AccessListResponse> listResponse = new List<AccessListResponse>();

            var accessList = _unitOfWork._Access.GetDbSet()
                .Include(f => f.User)
                .AsEnumerable()
                .Where(f => f.User.Id == userId)
                .ToList();

            foreach (var item in accessList)
            {
                AccessListResponse res = new AccessListResponse();
                res.AccessResponse = item.ToAccessResponse();

                var sites = await _unitOfWork._Sites.GetDbSet()
                    .Where(f => f.Id == item.AccessLevelId).FirstOrDefaultAsync();

                if (sites != null)
                    res.SitesResponse = sites.ToSitesResponse();

                var studyCountry = await _unitOfWork._StudyCountry.GetDbSet()
                    .Where(f => f.Id == item.AccessLevelId).FirstOrDefaultAsync();

                if (studyCountry != null)
                    res.StudyCountry = studyCountry;

                listResponse.Add(res);
            }
            return listResponse;
        }



    }
}
