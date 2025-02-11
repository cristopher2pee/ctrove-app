using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO
{

    public class CountryRequest : BaseRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Continent { get; set; }= string.Empty;

    }

    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

    public static class CountryResponseExtension
    {
        public static CountryResponse ToCountryResponse(this Country entity)
            => new CountryResponse
            {
                Id = entity.Id,
                Status = entity.Status,
                Code = entity.Code,
                Continent = entity.Continent,
                Name = entity.Name,
            };

        public static IEnumerable<CountryResponse> ToCountryResponseList(this IEnumerable<Country> entities)
            => entities.Select(e => e.ToCountryResponse()).ToList();
    }
}
