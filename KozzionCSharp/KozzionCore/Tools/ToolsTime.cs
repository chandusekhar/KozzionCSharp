using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Tools
{
    public static class ToolsTime
    {
        public static DateTime UnixTimeStampToDateTime(double unix_time_stamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime date_time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return date_time.AddSeconds(unix_time_stamp).ToLocalTime();
        }


        public static DateTime JavaTimeStampToDateTime(double java_time_stamp)
        {
            // Java timestamp is millisecods past epoch
            DateTime date_time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return date_time.AddSeconds(Math.Round(java_time_stamp / 1000)).ToLocalTime();
        }

        public static DateTimeUTC UnixTimeStampToDateTimeUTC(double unix_time_stamp)
        {
            // Unix timestamp is seconds past epoch
            DateTimeUTC date_time = new DateTimeUTC(1970, 1, 1);
            return date_time.AddSeconds(unix_time_stamp);
        }


        public static DateTimeUTC JavaTimeStampToDateTimeUTC(double java_time_stamp)
        {
            // Java timestamp is millisecods past epoch
            DateTimeUTC date_time = new DateTimeUTC(1970, 1, 1);
            return date_time.AddSeconds(Math.Round(java_time_stamp / 1000));
        }

        public static int DateTimeToUnixTimestampInt32(DateTime dateTime)
        {
            return (int)(TimeZoneInfo.ConvertTimeToUtc(dateTime) -  new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
