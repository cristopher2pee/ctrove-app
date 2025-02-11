using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO
{
    internal class VendorTypeDto
    {
    }

    public class VendorTypeRequest : BaseRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class VendorTypeResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

    public static class VendorTypeResponseExtension
    {
        public static VendorTypeResponse ToVendorTypeResponse(this VendorType entity)
            => new VendorTypeResponse
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Status = entity.Status,
            };

        public static IEnumerable<VendorTypeResponse> ToVendorTypeResponseList(this IEnumerable<VendorType> entities)
            => entities.Select(e => e.ToVendorTypeResponse()).ToList();
    }
}
