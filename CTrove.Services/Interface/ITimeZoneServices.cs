using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface ITimeZoneServices
    {
        DateTime ConvertToUserTimeZoneSettings(DateTime utc, string timezone);

        string ConvertIanaToWindows(string iana);

        DateTime ConvertUserTimeZoneSetingToUTC(DateTime utc, string timezone);
    }
}
