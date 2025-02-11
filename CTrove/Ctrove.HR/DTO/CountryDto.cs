using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.DTO
{
    internal class CountryDto
    {
    }

    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
    }
}
