using System.Globalization;

namespace TimeFreezesAtMidnight.Helpers;
internal static class TimeHelper
{
    internal const int DefaultValueTimeFreezesAt = 2400;

    private const int StartOfDay = 0600;

    private const int EndOfDay = 2600;

    private static readonly CultureInfo EnUs = CultureInfo.CreateSpecificCulture("en-US");

    private static readonly DateTime DummyToday = DateTime.Today;

    internal static int ClampTime(int value)
    {
        switch (value)
        {
            case < StartOfDay:
                return StartOfDay;
            case > EndOfDay:
                return EndOfDay;
        }

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
        if (!DateTime.TryParse(hTime, out var date))
            return DefaultValueTimeFreezesAt;
        
        var value = date.Hour * 100 + date.Minute;
        if (value < StartOfDay)
            value += 2400; // Far as the game's concerned, past midnight but before crashing is STILL on the same day

        return value;
    }

    private static string DateTimeToHumanReadableTime(DateTime dTime) =>
        dTime.ToString("h:mm tt", EnUs);

    internal static string GameTimeToHumanReadableTime(int gTime) =>
        DateTimeToHumanReadableTime(GameTimeToDate(gTime));

    private static DateTime GameTimeToDate(int gTime)
    {
        var hours = gTime / 100;
        var minutes = gTime % 100;
        return DummyToday.AddHours(hours).AddMinutes(minutes);
    }
}
