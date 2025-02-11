using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO
{
    internal class ContactTypeDto
    {
    }

    public class ContactTypeRequest : BaseRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ContactTypeResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

    public static class ContactTypeResponseExtension
    {
        public static ContactTypeResponse ToContactTypeResponse(this ContactType entity)
            => new ContactTypeResponse
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Status = entity.Status,
            };

        public static IEnumerable<ContactTypeResponse> ToContactTypeResponseList(this IEnumerable<ContactType> entities)
            => entities.Select(e => e.ToContactTypeResponse()).ToList();
    }
}
