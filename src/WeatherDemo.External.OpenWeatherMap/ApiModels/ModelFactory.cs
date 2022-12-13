using System.Text.Json;
using FluentAssertions;

namespace WeatherDemo.External.OpenWeatherMap.ApiModels
{
    internal static class ModelFactory
    {
        private static readonly JsonSerializerOptions serializarOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public static CurrentWeather? CreateCurrentWeather(string json)
        {
            json.Should().NotBeNullOrWhiteSpace();

            var model = JsonSerializer.Deserialize<CurrentWeather>(json, serializarOptions);

            return model;
        }

        public static Forecast5Days? CreateForecast(string json)
        {
            json.Should().NotBeNullOrWhiteSpace();

            var model = JsonSerializer.Deserialize<Forecast5Days>(json, serializarOptions);

            return model;
        }
        public static GeoData[]? CreateGeoData(string json)
        {
            json.Should().NotBeNullOrWhiteSpace();

            var model = JsonSerializer.Deserialize<GeoData[]>(json, serializarOptions);

            return model;
        }
    }
}
