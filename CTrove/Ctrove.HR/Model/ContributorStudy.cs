using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Model
{
    public class ContributorStudy
    {
        public Guid Id { get; set; }
        public Guid StudyId { get; set; }
        public string StudyName { get; set; } = string.Empty;
        public Guid ContributorId { get; set; }
    }
}
