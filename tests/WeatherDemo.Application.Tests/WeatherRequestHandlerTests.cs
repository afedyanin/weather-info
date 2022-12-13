using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RestEase;
using WeatherDemo.Application.Requests.Weather.ByGeoPoint;
using WeatherDemo.Application.Requests.Weather.BySearchTerm;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.External.OpenWeatherMap;
using WeatherDemo.External.OpenWeatherMap.Internal;
using WeatherDemo.External.OpenWeatherMap.Tests;
using WeatherDemo.Infrastructure.InMemory;

namespace WeatherDemo.Application.Tests
{
    [TestFixture(Category = "Integration", Explicit = true)]
    public class WeatherRequestHandlerTests
    {
        private IOpenWeatherMapClient _client;
        private IOpenWeatherMapService _service;
        private IGeoLocationRepository _geo;
        private IWeatherInfoCache _weather;
        private WeatherInfoCacheOptions _cacheOptions;
        private WeatherByGeoPointRequestHandler _pointHandler;
        private WeatherBySearchTermRequestHandler _termHandler;

        [SetUp]
        public void Setup()
        {
            _cacheOptions = new WeatherInfoCacheOptions() { ExpirationSeconds = 60 };
            _client = RestClient.For<IOpenWeatherMapClient>(TH.ApiOptions.BaseUrl);
            _service = new OpenWeatherMapService(Options.Create(TH.ApiOptions), _client);
            _geo = new GeoLocationRepositoryInMemory();
            _weather = new WeatherInfoCache(Options.Create(_cacheOptions), NullLogger<WeatherInfoCache>.Instance);
            _pointHandler = new WeatherByGeoPointRequestHandler(_service, _weather, _geo, NullLogger<WeatherByGeoPointRequestHandler>.Instance);
            _termHandler = new WeatherBySearchTermRequestHandler(_service, _weather, _geo, NullLogger<WeatherBySearchTermRequestHandler>.Instance);
        }

        [Test]
        public async Task CanHandleGeoPointRequest()
        {
            var request = new WeatherByGeoPointRequest(TH.Moscow);

            var res = await _pointHandler.Handle(request, CancellationToken.None);

            Assert.That(res, Is.Not.Null);

            Assert.That(res.GeoLocation, Is.Not.Null);
            res.GeoLocation.Dump();

            Assert.That(res.WeatherInfo, Is.Not.Null);
            res.WeatherInfo.Dump();
        }

        [Test]
        public async Task CanHandleGeoSearchRequest()
        {
            var request = new WeatherBySearchTermRequest("Новосибирск");

            var res = await _termHandler.Handle(request, CancellationToken.None);

            Assert.That(res, Is.Not.Null);

            Assert.That(res.GeoLocation, Is.Not.Null);
            res.GeoLocation.Dump();

            Assert.That(res.WeatherInfo, Is.Not.Null);
            res.WeatherInfo.Dump();
        }
    }
}
