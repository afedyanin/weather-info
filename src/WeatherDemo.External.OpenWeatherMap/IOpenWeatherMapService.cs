using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.External.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        Task<GeoLocation?> FindLocation(string query);

        Task<GeoLocation?> GetLocation(GeoPoint point);

        Task<WeatherInfo?> GetWeather(GeoPoint point);
    }
}
