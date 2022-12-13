namespace WeatherDemo.External.OpenWeatherMap
{
    public class OpenWeatherMapOptions 
    {
        public static readonly string SectionName = "OpenWeatherMap";

        public string BaseUrl { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;
    }
}
