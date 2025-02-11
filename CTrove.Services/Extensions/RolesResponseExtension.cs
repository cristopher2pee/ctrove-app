using CTrove.Core.DTO.Response;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Extensions
{
    public static class RolesResponseExtension
    {
        public static RolesResponse ToRolesResponse(this Roles e)
            => new RolesResponse
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Status = e.Status,
                Blinded = e.Blinded,
                RolesPages = e.RolesPages != null ? e.RolesPages.ToList() : null,
            };

        public static IEnumerable<RolesResponse> ToRolesListResponse(this IEnumerable<Roles> e)
            => e.Select(e => e.ToRolesResponse()).ToList();

    }
}
