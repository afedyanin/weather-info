namespace WeatherDemo.External.OpenWeatherMap.ApiModels
{
    /// <summary>
    /// https://openweathermap.org/api/geocoding-api
    /// </summary>
    public record GeoData(
        string Name,
        LocalNames Local_names,
        decimal Lat,
        decimal Lon,
        string Country,
        string State);

    public record LocalNames(
        string Ascii,
        string En,
        string Feature_name,
        string Ru);
}
