using RestEase;
using WeatherDemo.WebApi.Client.Models;

namespace WeatherDemo.WebApi.Client
{
    public interface IWeatherDemoApiClient
    {
        [Get("weather/point")]
        Task<WeatherInfoDto> GetForLocation([Query] decimal lat, [Query] decimal lon);

        [Get("weather/search")]
        Task<WeatherInfoDto> GetForQuery([Query] string query);

        [Get("images/point")]
        Task<GeoLocationImageDto> FindImage([Query] decimal lat, [Query] decimal lon);
    }
}
