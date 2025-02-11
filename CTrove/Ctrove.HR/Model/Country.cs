using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Model
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
    }
}
