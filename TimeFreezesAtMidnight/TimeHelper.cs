using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFreezesAtMidnight
{
    public class TimeHelper
    {
        public const int DefaultValueTimeFreezesAt = 2400;

        public const int StartOfDay = 0600;

        public const int EndOfDay = 2600;

        public const int TimeFreezeInterval = 30;

        private static readonly CultureInfo _enUS = CultureInfo.CreateSpecificCulture("en-US");

        private static readonly DateTime _dummyToday = DateTime.Today;

        public static int ClampTime(int value)
        {
            if (value < StartOfDay)
                return StartOfDay;

            if (value > EndOfDay)
                return EndOfDay;

            var rem100 = value % 100 - 50;
            if (rem100 > 0)
                value -= rem100;

            var rem10 = value % 10;
            if (rem10 > 0)
                value -= rem10;

            return value;
        }

        //public static string[] GetHumanReadableTimes()
        //{
        //    var results = new List<string>();
        //    var minDate = GameTimeToDate(StartOfDay);
        //    var maxDate = GameTimeToDate(EndOfDay);
        //    var interval = TimeSpan.FromMinutes(TimeFreezeInterval);
        //    for (var gTime = minDate; gTime <= maxDate; gTime += interval)
        //    {
        //        var hTime = DateTimeToHumanReadableTime(gTime);
        //        results.Add(hTime);
        //    }

        //    return results.ToArray();
        //}

        public static int HumanReadableTimeToGameTime(string hTime)
        {
            if (DateTime.TryParse(hTime, out DateTime date))
            {
                var value = date.Hour * 100 + date.Minute;
                if (value < StartOfDay)
                    value += 2400; // For anything past 1AM the game still treats it as part of the same day

                return value;
            }

            return DefaultValueTimeFreezesAt;
        }

        public static string DateTimeToHumanReadableTime(DateTime dTime)
        {
            return dTime.ToString("h:mm tt", _enUS);
        }

        public static string GameTimeToHumanReadableTime(int gTime)
        {
            var dTime = GameTimeToDate(gTime);
            return DateTimeToHumanReadableTime(dTime);
        }

        public static DateTime GameTimeToDate(int gTime)
        {
            var hours = gTime / 100;
            var minutes = gTime % 100;
            return _dummyToday.AddHours(hours).AddMinutes(minutes);
        }
    }
}
