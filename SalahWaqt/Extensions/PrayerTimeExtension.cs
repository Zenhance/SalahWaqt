using System.Globalization;
using SalahWaqt.Models;

namespace SalahWaqt.Extensions;

public static class PrayerTimeExtensions
{
    public static List<PrayerTime> ToPrayerTimeList(this Timings timings)
    {
        var prayers = new List<PrayerTime>
        {
            new() { Name = "Fajr", StartTime = ParseTime(timings.Fajr) },
            new() { Name = "Dhuhr", StartTime = ParseTime(timings.Dhuhr) },
            new() { Name = "Asr", StartTime = ParseTime(timings.Asr) },
            new() { Name = "Maghrib", StartTime = ParseTime(timings.Maghrib) },
            new() { Name = "Isha", StartTime = ParseTime(timings.Isha) },
            new() {Name = "Jum'ah", StartTime = ParseTime(timings.Dhuhr)}
        };

        return prayers;
    }

    private static DateTime ParseTime(string time)
    {
        return DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
    }
}