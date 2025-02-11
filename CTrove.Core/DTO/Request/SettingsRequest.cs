using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.DTO.Request
{
    public class SettingsRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TimeZone = string.Empty;

        public string Remarks = string.Empty;
        public string Location = string.Empty;
    }
}
