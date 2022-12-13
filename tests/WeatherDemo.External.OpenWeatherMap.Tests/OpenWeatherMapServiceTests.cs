using Microsoft.Extensions.Options;
using WeatherDemo.External.OpenWeatherMap.Internal;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    [TestFixture(Category = "Integration", Explicit = true)]
    internal class OpenWeatherMapServiceTests
    {
        private static readonly string cityName = "Москва";

        private IOptions<OpenWeatherMapOptions> _options;
        private IOpenWeatherMapClient _client;
        private IOpenWeatherMapService _service;

        [SetUp]
        public void Setup()
        {
            _options = Options.Create(TH.ApiOptions);
            _client = RestClient.For<IOpenWeatherMapClient>(_options.Value.BaseUrl);
            _service = new OpenWeatherMapService(_options, _client);
        }

        [Test]
        public async Task CanGetGeoData()
        {
            var geo = await _service.FindLocation(cityName);

            Assert.That(geo, Is.Not.Null);

            geo.Dump();
        }

        [Test]
        public async Task CanGetWeather()
        {
            var geo = await _service.FindLocation(cityName);
            Assert.That(geo, Is.Not.Null);

            var weather = await _service.GetWeather(geo.Point);
            Assert.That(weather, Is.Not.Null);

            weather.Dump();
        }
    }
}
