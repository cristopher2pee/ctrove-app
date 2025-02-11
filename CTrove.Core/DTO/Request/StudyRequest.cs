using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class StudyRequest : BaseRequest
    {
        public Guid? TherapeuticAreaId { get; set; }
        public string StudyType { get; set; } = string.Empty;
        public Guid? ClassificationId { get; set; }
        public string BillingCode { get; set; } = string.Empty;
        public string Sponsor { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ApiKeyToken { get; set; } = string.Empty;
    }
}
