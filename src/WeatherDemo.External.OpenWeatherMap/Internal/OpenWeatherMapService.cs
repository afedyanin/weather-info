using Microsoft.Extensions.Options;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.External.OpenWeatherMap.ApiModels;

namespace WeatherDemo.External.OpenWeatherMap.Internal
{
    internal class OpenWeatherMapService : IOpenWeatherMapService
    {
        private static readonly int GeoResponseCountLimit = 3;

        private readonly OpenWeatherMapOptions _options;
        private readonly IOpenWeatherMapClient _client;

        public OpenWeatherMapService(
            IOptions<OpenWeatherMapOptions> options,
            IOpenWeatherMapClient client)
        {
            _options = options.Value;
            _client = client;
        }

        public async Task<GeoLocation?> FindLocation(string query)
        {
            var json = await _client.GetGeo(q: query, limit: GeoResponseCountLimit, appid: _options.ApiKey);
            var apiModel = ModelFactory.CreateGeoData(json);
            var geoData = apiModel?.FirstOrDefault();
            var gl = geoData?.ToGeoLocation();

            return gl;
        }

        public async Task<GeoLocation?> GetLocation(GeoPoint point)
        {
            var (lat, lon) = point.Format();
            var json = await _client.GetGeoReverse(lat: lat, lon: lon, limit: GeoResponseCountLimit, appid: _options.ApiKey);
            var apiModel = ModelFactory.CreateGeoData(json);
            var geoData = apiModel?.FirstOrDefault();
            var gl = geoData?.ToGeoLocation();

            return gl;
        }

        public async Task<WeatherInfo?> GetWeather(GeoPoint point)
        {
            var (lat, lon) = point.Format();
            var jsonCurrent = await _client.GetWeather(lat: lat, lon: lon, appid: _options.ApiKey);
            var current = ModelFactory.CreateCurrentWeather(jsonCurrent);

            var jsonForecast = await _client.GetForecast(lat: lat, lon: lon, appid: _options.ApiKey);
            var forecast = ModelFactory.CreateForecast(jsonForecast);

            var wi = current?.ToWeatherInfo(forecast, point);

            return wi;
        }
    }
}
