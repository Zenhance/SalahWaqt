namespace SalahWaqt.Models;

public class PrayerTime
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int AdjustmentHours { get; set; }
    public int AdjustmentMinutes { get; set; }
    
    public DateTime AdjustedStartTime => StartTime.AddHours(AdjustmentHours).AddMinutes(AdjustmentMinutes);
}