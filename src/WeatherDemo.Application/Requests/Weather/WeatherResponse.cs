using WeatherDemo.Domain.Entities;

namespace WeatherDemo.Application.Requests.Weather
{
    public record WeatherResponse
    {
        public WeatherInfo WeatherInfo { get; init; }

        public GeoLocation GeoLocation { get; init; }

        public WeatherResponse(WeatherInfo weatherInfo, GeoLocation geoLocation)
        {
            WeatherInfo = weatherInfo;
            GeoLocation = geoLocation;
        }
    }
}
