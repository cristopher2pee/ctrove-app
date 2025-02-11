using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Entity;
using CTrove.Core.DTO.Response;

namespace CTrove.Services.Extensions
{
    public static class UserResponseExtension
    {
        public static UserResponse ToUserResponse(this User entity)
        => new UserResponse
        {
            Id = entity.Id,
            ObjectId = entity.ObjectId,
            Prefix = entity.Prefix,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            MiddleName = entity.Middlename,
            Suffix = entity.Suffix,
            Email = entity.Email,
            Mobile = entity.Mobile,
            Landline = entity.Landline,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Organization = entity.Organization,
            RolesId = entity.RolesId,
            Roles = entity.Roles != null ?
                new Roles
                {
                    Blinded = entity.Roles.Blinded,
                    Status = entity.Roles.Status,
                    Code = entity.Roles.Code,
                    Id = entity.Roles.Id,
                    Name = entity.Roles.Name,
                } : null,
            Status = entity.Status,
        };

        public static List<UserResponse> ToUserResponseList(this List<User> entities)
        {
            var res = entities.Select(f => f.ToUserResponse()).ToList();
            return res;
        }

        public static IEnumerable<UserResponse> ToUserListResponse(this IOrderedEnumerable<User> employee)
           => employee.Select(e => e.ToUserResponse()).ToList();

    }
}
