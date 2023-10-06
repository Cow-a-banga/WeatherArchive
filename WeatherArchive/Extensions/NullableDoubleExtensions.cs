namespace WeatherArchive.Extensions;

public static class NullableDoubleExtensions
{
    public static int? ToNullableInt(this double? d)
    {
        return d.HasValue ? (int)d.Value : null;
    }
}