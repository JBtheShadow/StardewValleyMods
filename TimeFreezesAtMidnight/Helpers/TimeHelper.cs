using System.Globalization;

namespace TimeFreezesAtMidnight.Helpers;
internal sealed class TimeHelper
{
    internal const int DefaultValueTimeFreezesAt = 2400;

    internal const int StartOfDay = 0600;

    internal const int EndOfDay = 2600;

    internal const int TimeFreezeInterval = 30;

    private static readonly CultureInfo _enUS = CultureInfo.CreateSpecificCulture("en-US");

    private static readonly DateTime _dummyToday = DateTime.Today;

    internal static int ClampTime(int value)
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

    internal static int HumanReadableTimeToGameTime(string hTime)
    {
        if (DateTime.TryParse(hTime, out DateTime date))
        {
            var value = date.Hour * 100 + date.Minute;
            if (value < StartOfDay)
                value += 2400; // Far as the game's concerned, past midnight but before crashing is STILL on the same day

            return value;
        }

        return DefaultValueTimeFreezesAt;
    }

    internal static string DateTimeToHumanReadableTime(DateTime dTime) =>
        dTime.ToString("h:mm tt", _enUS);

    internal static string GameTimeToHumanReadableTime(int gTime) =>
        DateTimeToHumanReadableTime(GameTimeToDate(gTime));

    internal static DateTime GameTimeToDate(int gTime)
    {
        var hours = gTime / 100;
        var minutes = gTime % 100;
        return _dummyToday.AddHours(hours).AddMinutes(minutes);
    }
}
