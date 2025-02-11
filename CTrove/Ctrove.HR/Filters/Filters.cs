using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Filters
{
    internal class Filters
    {
    }

    //public class BaseFilters
    //{
    //    [Range(1, int.MaxValue, ErrorMessage = "Current Page must be a positive number larger or equal to 1")]
    //    public int Page { get; set; } = 1;
    //    public int Limit { get; set; } = 50;
    //    public string Search { get; set; } = string.Empty;
    //}
    //public class ContributorFilters : BaseFilters
    //{
    //    public List<Guid>? CountryIds { get; set; }
    //    public List<Guid>? OrganizationIds { get; set; }
    //    public bool Active { get; set; } = true;
    //}

    public class GetRequest
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
    }

}
