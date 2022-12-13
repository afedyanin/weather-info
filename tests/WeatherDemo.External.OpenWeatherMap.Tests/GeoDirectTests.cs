using WeatherDemo.External.OpenWeatherMap.ApiModels;
using WeatherDemo.External.OpenWeatherMap.Internal;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    [TestFixture(Category = "Integration", Explicit = true)]
    public class GeoDirectTests
    {
        [Test]
        public void CanDeserializeLocalNames()
        {
            var json = Resources.MosLocalNames;

            var model = JsonSerializer.Deserialize<LocalNames>(json, TH.DeserializerOptions);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Ru, Is.EqualTo("Москва"));

            var serialized = JsonSerializer.Serialize(model, TH.SerializerOptions);
            Assert.That(serialized, Is.Not.Null);
        }

        [Test]
        public void CanDeserializeGeoDirect()
        {
            var json = Resources.GeoDirectMoscow;

            var model = JsonSerializer.Deserialize<GeoData[]>(json, TH.DeserializerOptions);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize<GeoData[]>(model, TH.SerializerOptions);
            Assert.That(serialized, Is.Not.Null);
        }

        [Test]
        public void CanCreateGeoLocation()
        {
            var model = JsonSerializer.Deserialize<GeoData[]>(Resources.GeoDirectMoscow, TH.DeserializerOptions);
            var geoData = model?.FirstOrDefault();

            Assert.That(geoData, Is.Not.Null);

            var res = geoData.ToGeoLocation();
            Assert.That(res, Is.Not.Null);

            res.Dump();
        }

        [Test]
        public async Task CanFetchGeoDirect()
        {
            var client = RestClient.For<IOpenWeatherMapClient>(TH.ApiOptions.BaseUrl);

            var jsonString = await client.GetGeo(q: "Moscow", limit: 5, appid: TH.ApiOptions.ApiKey);

            Assert.That(jsonString, Is.Not.Null);

            var model = ModelFactory.CreateGeoData(jsonString);
            Assert.That(model, Is.Not.Null);

            var serialized = JsonSerializer.Serialize(model);
            Assert.That(serialized, Is.Not.Null);
            Assert.That(serialized, Is.Not.Empty);
        }
    }
}
