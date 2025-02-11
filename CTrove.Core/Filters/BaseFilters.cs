using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Filters
{
    public class BaseFilters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Current Page must be a positive number larger or equal to 1")]
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 50;
        public string Search { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
             
    }
}
