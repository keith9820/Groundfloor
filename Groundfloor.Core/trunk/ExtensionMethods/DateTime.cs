namespace System
{
    public static class DateTimeExtensions
    {
        public static bool isWeekend(this DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);
        }
        public static bool isWeekday(this DateTime dt)
        {
            return !dt.isWeekend();
        }

        public static bool inRange(this DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }
        /// <summary>
        /// Does the current date fall within range of dates calculated from the given hour value
        /// </summary>
        /// <param name="dt">the current datetime</param>
        /// <param name="startHour">the start of day hour (24-hour format)</param>
        /// <param name="endHour">the end of day hour (24-hour format)</param>
        /// <returns></returns>
        public static bool inRange(this DateTime dt, ushort startHour, ushort endHour)
        {
            DateTime start = dt.StartOfDay().AddHours(startHour);
            DateTime end = dt.StartOfDay().AddHours(endHour);
            return dt.inRange(start, end);
        }

        /// <summary>
        /// Return a DateTime value 
        /// </summary>
        /// <param name="dt">the current datetime</param>
        /// <param name="time">the time of day in 24-hour format (i.e. "21:30")</param>
        /// <returns></returns>
        public static DateTime DateFromTimeString(this DateTime dt, string time)
        {
            var parts = time.Split(':');
            DateTime ret = dt.AddHours(parts[0].ToDouble());
            if (parts.Length == 2)
                ret = ret.AddMinutes(parts[1].ToDouble());

            return ret;
        }
        
        public static DateTime DateFromUnixTime(this int unixTime)
        {
            long l = unixTime;
            return l.DateFromUnixTime();
        }
        public static DateTime DateFromUnixTime(this long unixTime)
        {
            var dt = new DateTime(1970, 1, 1, 1, 0, 0, 0, 0);
            return dt.AddSeconds(unixTime).ToLocalTime();
        }

        public static DateTime DateFromUtc(this DateTime dt)
        {
            DateTime convertedDate = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            return convertedDate.ToLocalTime();
        }

        public static double ToJulian(this DateTime dt)
        {
            int y = dt.Year;
            int m = dt.Month;
            int d = dt.Day;
            int h = dt.Hour;
            int mn = dt.Minute;
            int s = dt.Second;

            double jy;
            double ja;
            double jm;

            if (m > 2)
            {
                jy = y;
                jm = m + 1;
            }
            else
            {
                jy = y - 1;
                jm = m + 13;
            }

            double intgr = Math.Floor(Math.Floor(365.25 * jy) + Math.Floor(30.6001 * jm) + d + 1720995);

            //check for switch to Gregorian calendar   
            int gregcal = 15 + 31 * (10 + 12 * 1582);
            if (d + 31 * (m + 12 * y) >= gregcal)
            {
                ja = Math.Floor(0.01 * jy);
                intgr += 2 - ja + Math.Floor(0.25 * ja);
            }

            //correct for half-day offset   
            double dayfrac = h / 24.0 - 0.5;
            if (dayfrac < 0.0)
            {
                dayfrac += 1.0;
                --intgr;
            }

            //now set the fraction of a day   
            double frac = dayfrac + (mn + s / 60.0) / 60.0 / 24.0;

            //round to nearest second   
            double jd0 = (intgr + frac) * 100000;
            double jd = Math.Floor(jd0);
            if (jd0 - jd > 0.5) ++jd;

            return jd / 100000;
        }

        /// <summary>
        /// Set time portion to start of dat 00:00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime dt)
        {
            return DateTime.Parse(dt.ToShortDateString());
        }
        /// <summary>
        /// Set time portion to end of day (midnight) 12:59:59
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime dt)
        {
            return DateTime.Parse(dt.ToShortDateString()).AddDays(1).AddSeconds(-1);
        }

        public static string ToShortDateString(this DateTime? dt)
        {
            if (dt == null)
                return "";

            return dt.Value.ToShortDateString();
        }
        public static string ToString(this DateTime? dt)
        {
            if (dt == null)
                return "";

            return dt.Value.ToString();
        }
        public static string ToString(this DateTime? dt, string format)
        {
            if (dt == null)
                return "";

            return dt.Value.ToString(format);
        }

        public static DateTime ToValidShippingDate(this DateTime date)
        {
            DateTime validDate = date;

            if (validDate.DayOfWeek == DayOfWeek.Saturday)
                validDate = validDate.AddDays(2);

            if (validDate.DayOfWeek == DayOfWeek.Sunday)
                validDate = validDate.AddDays(1);

            return validDate;
        }

        public static string Default(this DateTime date, string defaultText)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return defaultText;
            else
                return date.ToString();
        }
    }
}
