using MediatR;
using Microsoft.Extensions.Logging;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Exeptions;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.External.OpenWeatherMap;

namespace WeatherDemo.Application.Requests.Weather.ByGeoPoint
{
    public class WeatherByGeoPointRequestHandler : WeatherRequestHandler,
        IRequestHandler<WeatherByGeoPointRequest, WeatherResponse>
    {
        public WeatherByGeoPointRequestHandler(
            IOpenWeatherMapService service,
            IWeatherInfoCache weatherCache,
            IGeoLocationRepository geoRepo,
            ILogger<WeatherByGeoPointRequestHandler> logger)
            : base (service, weatherCache, geoRepo, logger)
        {
        }

        public async Task<WeatherResponse> Handle(WeatherByGeoPointRequest request, CancellationToken cancellationToken)
        {
            var location = await GetGeoLocationByPoint(request.GeoPoint);

            if (location == null)
            {
                throw new BadRequestException($"No geodata found for requested Point={request.GeoPoint.Format()}");
            }

            var weather = await GetWeatherInfo(request.GeoPoint, request.TimeStamp);

            return new WeatherResponse(weather, location);
        }

        protected async Task<GeoLocation?> GetGeoLocationByPoint(GeoPoint point)
        {
            var cached = GeoRepo.GetByGeoPoint(point);

            if (cached != null)
            {
                Logger.LogInformation($"Found cached GeoLocation in DB: Name={cached.Name} Point={point.Format()}");
                return cached;
            }

            Logger.LogWarning($"Calling external API to get actual GetLocation Point={point.Format()}");
            var actual = await Service.GetLocation(point);

            if (actual == null)
            {
                Logger.LogWarning($"No actual GetLocation found. Point={point.Format()}");
                return null;
            }

            // Сервис может вернуть измененные данные по координатам
            // Нужно сохранить исходные координаты, чтобы избежать дублей в БД
            actual.Point = point;

            Logger.LogInformation($"Saving new GetLocation to cache. Name={actual.Name} Point={point.Format()}");
            GeoRepo.Save(actual);

            return actual;
        }
    }
}
