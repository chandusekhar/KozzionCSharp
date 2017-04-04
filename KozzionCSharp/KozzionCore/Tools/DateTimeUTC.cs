using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Tools
{
    // This class exists to make sure we are using UTC, I might seem elaborate but I hope it is a good best way to enforce this
    // Note that ticks ignore leap seconds so they are a good coordinate system
    public class DateTimeUTC : IComparable<DateTimeUTC>
    {
        public DateTime InnerDateTime { get; private set; }

        public DayOfWeek DayOfWeek { get { return InnerDateTime.DayOfWeek; } } // Sunday = 0,

        public int Year { get { return InnerDateTime.Year; } }   
        public int Month { get { return InnerDateTime.Month; } }
        public int Day { get { return InnerDateTime.Day; } }
        public int Hour { get { return InnerDateTime.Hour; } }
        public int Minute { get { return InnerDateTime.Minute; } }
        public int Second { get { return InnerDateTime.Second; } }

        public long Ticks { get { return InnerDateTime.Ticks; } }
        public DateTimeKind Kind { get { return InnerDateTime.Kind; } }

        public static DateTimeUTC Now { get { return new DateTimeUTC(DateTime.Now.ToUniversalTime()); } }

        public DateTimeUTC Date { get { return new DateTimeUTC(Year, Month, Day); } }

   



        // public int DayOfWeek { get { return InnerDateTime.Week; } }

        public DateTimeUTC(int year, int month, int day, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            InnerDateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
        }

        public DateTimeUTC(DateTime date_time, TimeZoneInfo time_zone)
        {
            InnerDateTime = TimeZoneInfo.ConvertTimeToUtc(date_time, time_zone);
        }

        public DateTimeUTC(DateTime date_time)
        {
            if (!date_time.Equals(date_time.ToUniversalTime()))
            {
                throw new Exception("Only use UTC");
            }
            InnerDateTime = date_time.ToUniversalTime();
        }

        public DateTimeUTC()
          :  this(1970, 1,1)
        {
        }

        public DateTimeUTC AddYears(int years)
        {
            return new DateTimeUTC(InnerDateTime.AddYears(years));
        }

        public DateTimeUTC AddMonths(int months)
        {
            return new DateTimeUTC(InnerDateTime.AddMonths(months));
        }

        public DateTimeUTC AddDays(double days)
        {
            return new DateTimeUTC(InnerDateTime.AddDays(days));
        }

        public DateTimeUTC AddHours(double hours)
        {
            return new DateTimeUTC(InnerDateTime.AddHours(hours));
        }

        public DateTimeUTC AddMinutes(double minutes)
        {
            return new DateTimeUTC(InnerDateTime.AddMinutes(minutes));
        }

        public DateTimeUTC AddSeconds(double seconds)
        {
            return new DateTimeUTC(InnerDateTime.AddSeconds(seconds));
        }

        public DateTimeUTC AddMilliseconds(double milliseconds)
        {
            return new DateTimeUTC(InnerDateTime.AddMilliseconds(milliseconds));
        }

        public DateTimeUTC GetDayEqualOrBefore(DayOfWeek day_of_week)
        {
            int days_back = day_of_week - DayOfWeek;
            if (0 < days_back)
            {
                days_back -= 7;
            }
            return AddDays(days_back);
        }


        public bool Equals(object other)
        {
            if (other is DateTimeUTC)
            {
                return InnerDateTime.Equals(((DateTimeUTC)other).InnerDateTime);
            }
            return false;           
        }


        public int CompareTo(DateTimeUTC other)
        {
            return InnerDateTime.CompareTo(other.InnerDateTime);
        }

        public static bool operator <(DateTimeUTC object_0, DateTimeUTC object_1)
        {
            return object_0.CompareTo(object_1) == -1;
        }

        public static bool operator >(DateTimeUTC object_0, DateTimeUTC object_1)
        {
            return object_0.CompareTo(object_1) == 1;
        }


        public static bool operator <=(DateTimeUTC object_0, DateTimeUTC object_1)
        {
            return object_0.CompareTo(object_1) != 1;
        }

        public static bool operator >=(DateTimeUTC object_0, DateTimeUTC object_1)
        {
            return object_0.CompareTo(object_1) != -1;
        }

      

        public static TimeSpan operator -(DateTimeUTC object_0, DateTimeUTC object_1)
        {
            return object_0.InnerDateTime - object_1.InnerDateTime;
        }

        public override string ToString()
        {
            return InnerDateTime.ToString();
        }

        public string ToString(string formating)
        {
            return InnerDateTime.ToString(formating);
        }

        public static DateTimeUTC Parse(string value)
        {
            DateTime date_time = DateTime.Parse(value);
            return new DateTimeUTC(date_time.Year, date_time.Month, date_time.Day, date_time.Hour, date_time.Minute, date_time.Second, date_time.Millisecond);
        }


    }


}
