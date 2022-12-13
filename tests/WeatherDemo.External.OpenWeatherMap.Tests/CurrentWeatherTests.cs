using WeatherDemo.External.OpenWeatherMap.ApiModels;
using WeatherDemo.External.OpenWeatherMap.Internal;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    [TestFixture(Category = "Integration", Explicit = true)]
    public class CurrentWeatherTests
    {
        [Test]
        public void CanDeserializeWeather()
        {
            var json = Resources.CurrentWeatherMos161122;

            var model = JsonSerializer.Deserialize<CurrentWeather>(json, TH.DeserializerOptions);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize<CurrentWeather>(model, TH.SerializerOptions);
            Assert.That(serialized, Is.EqualTo(json));
        }

        [Test]
        public async Task CanFetchWeather()
        {
            var client = RestClient.For<IOpenWeatherMapClient>(TH.ApiOptions.BaseUrl);

            var (lat, lon) = TH.Moscow.Format();
            var jsonString = await client.GetWeather(lat: lat, lon: lon, appid: TH.ApiOptions.ApiKey);
            Assert.That(jsonString, Is.Not.Null);

            var model = ModelFactory.CreateCurrentWeather(jsonString);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize(model);
            Assert.That(serialized, Is.Not.Null);
            Assert.That(serialized, Is.Not.Empty);
        }
    }
}
