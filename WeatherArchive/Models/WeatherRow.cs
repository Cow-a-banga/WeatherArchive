using System.ComponentModel.DataAnnotations;

namespace WeatherArchive.Models;

public class WeatherRow
{
    public int Id { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public TimeSpan Time { get; set; }
    public double? Temperature { get; set; }
    public int? RelativeHumidity { get; set; }
    public double? DewPoint { get; set; }
    public int? AtmospherePressure { get; set; }
    public string? WindDirection { get; set; }
    public int? WindSpeed { get; set; }
    public int? Cloudy { get; set; }
    public int? CloudBase { get; set; }
    public int? HorizontalVisibility { get; set; }
    public string? WeatherConditions { get; set; }
    
}