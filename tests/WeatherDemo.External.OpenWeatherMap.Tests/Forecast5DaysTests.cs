using WeatherDemo.Domain.Shared.Utils;
using WeatherDemo.External.OpenWeatherMap.ApiModels;
using WeatherDemo.External.OpenWeatherMap.Internal;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    [TestFixture(Category ="Integration", Explicit = true)]
    public class Forecast5DaysTests
    {
        [Test]
        public void CanDeserializeForecast()
        {
            var json = Resources.Forecast5DaysMos161122;

            var model = JsonSerializer.Deserialize<Forecast5Days>(json, TH.DeserializerOptions);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize<Forecast5Days>(model, TH.SerializerOptions);
            Assert.That(serialized, Is.Not.Null);
        }

        [Test]
        public async Task CanFetchForecast()
        {
            var client = RestClient.For<IOpenWeatherMapClient>(TH.ApiOptions.BaseUrl);

            var (lat, lon) = TH.Moscow.Format();
            var jsonString = await client.GetForecast(lat: lat, lon: lon, appid: TH.ApiOptions.ApiKey);
            Assert.That(jsonString, Is.Not.Null);

            var model = ModelFactory.CreateForecast(jsonString);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize(model);
            Assert.That(serialized, Is.Not.Null);
            Assert.That(serialized, Is.Not.Empty);
        }

        [Test]
        public void CanGroupWeatherListItem()
        {
            var json = Resources.Forecast5DaysMos161122;

            var model = JsonSerializer.Deserialize<Forecast5Days>(json, TH.DeserializerOptions);
            Assert.That(model, Is.Not.Null);

            var items = model.List
                .GroupBy(k => UnixTime.UnixSecondsToDateTime(k.Dt).Day)
                .Select(g => new
                {
                    First = g.OrderBy(d => d.Dt).First(),
                    MaxTemp = g.Max(d => d.Main.Temp_max),
                    MinTemp = g.Min(d => d.Main.Temp_min),
                });

            Assert.That(items.Count, Is.EqualTo(5));
        }
    }
}
