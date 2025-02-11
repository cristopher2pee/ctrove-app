using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using CTrove.Core.Common;
using CTrove.Core.Filters;

namespace CTrove.Services.Interface
{
    public interface IUserServices
    {
        Task<UserResponse> isUserOnBoarding(Guid userId);
        Task<UserResponse> UserOnBoarding(Guid userId, UserRequest request);
        Task<UserResponse> GetProfile(Guid userId, string email);

        Task<UserAccessResponse?> GetUserProfile(Guid userId, string email);
        Task<UserResponse?> Update(Guid objectId,UserRequest request);
        Task<bool> Delete(Guid userId);

        Task<UserResponse?> Add(Guid objectId, UserRequest req);
        Task<PagedResult<UserResponse>> GetAll(UserFilters filters);
        Task<UserResponse> GetById(Guid objId, Guid id);

        Task<UserResponse> GetByEmail(string email);

        Task<UserResponse> InviteUserSave(AccountsRequest req, Guid objectId);

        Task<List<AccessListResponse>> UserAccessRights(Guid userId);

        Task<bool> Deactivate(DeactivateRequest id, Guid objctId);

        Task<bool> IsUserEmailExist(string email);
        
    }
}
