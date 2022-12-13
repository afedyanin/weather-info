using Microsoft.Extensions.Logging;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Exeptions;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.External.OpenWeatherMap;

namespace WeatherDemo.Application.Requests.Weather
{
    public abstract class WeatherRequestHandler 
    {
        protected IOpenWeatherMapService Service { get; init; }

        protected IWeatherInfoCache WeatherCache { get; init; }

        protected IGeoLocationRepository GeoRepo { get; init; }

        protected ILogger<WeatherRequestHandler> Logger { get; init; }

        protected WeatherRequestHandler(
            IOpenWeatherMapService service,
            IWeatherInfoCache weatherCache,
            IGeoLocationRepository geoRepo,
            ILogger<WeatherRequestHandler> logger)
        {
            Service = service;
            WeatherCache = weatherCache;
            GeoRepo = geoRepo;
            Logger = logger;
        }

        protected async Task<WeatherInfo> GetWeatherInfo(GeoPoint point, DateTime timeStamp)
        {
            var cached = WeatherCache.Get(point, timeStamp);

            if (cached != null)
            {
                Logger.LogInformation($"Get WeatherInfo from cache. Point={point.Format()}");
                return cached;
            }

            Logger.LogWarning($"Calling external API to get actual WeatherInfo Point={point.Format()}");
            var actual = await Service.GetWeather(point);

            if (actual == null)
            {
                throw new BadRequestException($"No geodata WeatherInfo for requested Point={point.Format()}");
            }

            Logger.LogInformation($"Saving new WeatherInfo to cache. Point={point.Format()}");
            WeatherCache.Set(actual);

            return actual;
        }
    }
}
