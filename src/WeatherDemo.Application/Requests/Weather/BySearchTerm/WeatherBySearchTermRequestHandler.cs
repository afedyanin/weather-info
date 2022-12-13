using MediatR;
using Microsoft.Extensions.Logging;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Exeptions;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.External.OpenWeatherMap;

namespace WeatherDemo.Application.Requests.Weather.BySearchTerm
{
    public class WeatherBySearchTermRequestHandler : WeatherRequestHandler,
        IRequestHandler<WeatherBySearchTermRequest, WeatherResponse>
    {
        public WeatherBySearchTermRequestHandler(
            IOpenWeatherMapService service,
            IWeatherInfoCache weatherCache,
            IGeoLocationRepository geoRepo,
            ILogger<WeatherBySearchTermRequestHandler> logger) :
            base(service, weatherCache, geoRepo, logger)
        {
        }

        public async Task<WeatherResponse> Handle(WeatherBySearchTermRequest request, CancellationToken cancellationToken)
        {
            var location = await GetGeoLocationByTerm(request.Term);

            if (location == null)
            {
                throw new BadRequestException($"No geodata found for requested Term={request.Term}");
            }

            var weather = await GetWeatherInfo(location.Point, request.TimeStamp);

            return new WeatherResponse(weather, location);
        }

        protected async Task<GeoLocation?> GetGeoLocationByTerm(string term)
        {
            var cached = GeoRepo.Find(term).FirstOrDefault();

            if (cached != null)
            {
                Logger.LogInformation($"Found cached GeoLocation in DB: Name={cached.Name} Term={term}");
                return cached;
            }

            Logger.LogWarning($"Calling external API to get actual GetLocation Term={term}");
            var actual = await Service.FindLocation(term);

            if (actual == null)
            {
                Logger.LogWarning($"No actual GetLocation found. Term={term}");
                return null;
            }

            Logger.LogInformation($"Saving new GetLocation to cache. Name={actual.Name}");
            GeoRepo.Save(actual);

            return actual;
        }
    }
}
