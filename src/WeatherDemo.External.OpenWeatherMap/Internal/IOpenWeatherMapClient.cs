using System.Runtime.CompilerServices;
using RestEase;

[assembly: InternalsVisibleTo("WeatherDemo.External.OpenWeatherMap.Tests")]
[assembly: InternalsVisibleTo("WeatherDemo.Application.Tests")]
[assembly: InternalsVisibleTo(RestClient.FactoryAssemblyName)]

namespace WeatherDemo.External.OpenWeatherMap.Internal
{
    /// <summary>
    /// https://openweathermap.org
    /// </summary>
    internal interface IOpenWeatherMapClient
    {
        [Get("data/2.5/weather")]
        Task<string> GetWeather([Query] string lat, [Query] string lon, [Query] string appid);

        [Get("data/2.5/forecast")]
        Task<string> GetForecast([Query] string lat, [Query] string lon, [Query] string appid);

        [Get("geo/1.0/direct")]
        Task<string> GetGeo([Query] string q, [Query] int limit, [Query] string appid);

        [Get("geo/1.0/reverse")]
        Task<string> GetGeoReverse([Query] string lat, [Query] string lon, [Query] int limit, [Query] string appid);
    }
}
