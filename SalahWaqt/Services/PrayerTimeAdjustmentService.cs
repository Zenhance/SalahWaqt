using Microsoft.JSInterop;
using System.Text.Json;
using SalahWaqt.Models;

namespace SalahWaqt.Services;

public class PrayerTimeAdjustmentService
{
    private readonly IJSRuntime _jsRuntime;
    private const string StorageKey = "prayer_adjustments";

    public PrayerTimeAdjustmentService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveAdjustments(Dictionary<string, TimeAdjustment> adjustments)
    {
        try
        {
            var json = JsonSerializer.Serialize(adjustments);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving adjustments: {ex.Message}");
            throw;
        }
    }

    public async Task<Dictionary<string, TimeAdjustment>> LoadAdjustments()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<string, TimeAdjustment>();
            }
            
            return JsonSerializer.Deserialize<Dictionary<string, TimeAdjustment>>(json) 
                   ?? new Dictionary<string, TimeAdjustment>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading adjustments: {ex.Message}");
            return new Dictionary<string, TimeAdjustment>();
        }
    }
}