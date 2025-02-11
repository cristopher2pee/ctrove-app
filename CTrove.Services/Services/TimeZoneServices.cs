using CTrove.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace CTrove.Services.Services
{
    public class TimeZoneServices : ITimeZoneServices
    {
        public DateTime ConvertToUserTimeZoneSettings(DateTime utc, string timezone)
        {
            var windowsTimeZone = ConvertIanaToWindows(timezone);
            TimeZoneInfo tmz = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZone);

            if (tmz != null)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(utc, tmz);
            }
            return DateTime.MinValue ;
        }

        public string ConvertIanaToWindows(string iana)
        {
            return TZConvert.IanaToWindows(iana);
        }

        public DateTime ConvertUserTimeZoneSetingToUTC(DateTime date, string timezone)
        {
            var windowsTimeZone = ConvertIanaToWindows(timezone);
            TimeZoneInfo tmz = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZone);

            if (tmz != null)
            {
                return TimeZoneInfo.ConvertTimeToUtc(date, tmz);
            }
            return DateTime.MinValue;
        }
    }
}
