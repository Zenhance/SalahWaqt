using System.Net.Http.Json;
using SalahWaqt.Extensions;
using SalahWaqt.Models;
using SalahWaqt.Services;

namespace SalahWaqt.Services;

public class PrayerTimeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PrayerTimeService> _logger;
    private readonly PrayerTimeAdjustmentService _adjustmentService;

    public PrayerTimeService(HttpClient httpClient, ILogger<PrayerTimeService> logger, PrayerTimeAdjustmentService adjustmentService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _adjustmentService = adjustmentService;
    }

    public async Task<List<PrayerTime>> GetPrayerTimes(string latitude, string longitude)
    {
        try
        {
            var apiUrl = $"https://api.aladhan.com/v1/timings?latitude={latitude}&longitude={longitude}";
            _logger.LogInformation("Fetching prayer times from {ApiUrl}", apiUrl);

            var response = await _httpClient.GetFromJsonAsync<AladhanResponse>(apiUrl);
            var prayers = response?.Data.Timings.ToPrayerTimeList() ?? new List<PrayerTime>();
        
            // Load and apply adjustments
            var adjustments = await _adjustmentService.LoadAdjustments();
            foreach (var prayer in prayers)
            {
                if (adjustments.TryGetValue(prayer.Name, out var adjustment))
                {
                    prayer.AdjustmentHours = adjustment.Hours;
                    prayer.AdjustmentMinutes = adjustment.Minutes;
                    // Update the StartTime to include adjustments
                    prayer.StartTime = prayer.StartTime.AddHours(adjustment.Hours).AddMinutes(adjustment.Minutes);
                }
            }

            return prayers.OrderBy(p => p.StartTime).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}