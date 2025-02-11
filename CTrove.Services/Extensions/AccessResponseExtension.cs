using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Entity;
using CTrove.Core.DTO.Response;

namespace CTrove.Services.Extensions
{
    public static class AccessResponseExtension
    {
        public static AccessResponse ToAccessResponse(this Access entity)
        {
            return new AccessResponse
            {
                Id = entity.Id,
                AccessLevelId = entity.AccessLevelId,
                Rights = entity.Rights,
                Status = entity.Status,
                UserId = entity.UserId,
                User = entity.User != null ? entity.User : null,
            };
        }


        public static IEnumerable<AccessResponse> ToAccessResponseList(IEnumerable<Access> entity)
            => entity.Select(e => e.ToAccessResponse()).ToList();

        public static List<AccessResponse> ToAccessResponseList(this List<Access> entities)
        {
            var res = entities.Select(f => f.ToAccessResponse()).ToList();
            return res;
        }

        public static IEnumerable<AccessResponse> ToAccessListResponse(this IEnumerable<Access> employee)
            => employee.Select(e => e.ToAccessResponse()).ToList();

    }
}
