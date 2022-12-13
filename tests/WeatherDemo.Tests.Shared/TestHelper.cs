using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.Infrastructure.InMemory;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    public static class TH
    {
        public static readonly GeoPoint Moscow = new(55.7504461M, 37.6174943M);

        public static readonly OpenWeatherMapOptions ApiOptions = new ()
        {
            BaseUrl = "https://api.openweathermap.org",
            ApiKey = "API KEY",
        };

        private static readonly MemoryCacheOptions _memoryCacheOptions = new()
        {
            SizeLimit = 1024
        };

        private static readonly WeatherInfoCacheOptions _weatherInfoCacheOptions = new()
        {
            SizeLimit = 1024,
            ExpirationSeconds = 2,
        };

        public static readonly IOptions<MemoryCacheOptions> MemoryCacheOptions = Options.Create(_memoryCacheOptions);

        public static readonly IOptions<WeatherInfoCacheOptions> WeatherInfoCacheOptions = Options.Create(_weatherInfoCacheOptions);

        public static readonly JsonSerializerOptions DeserializerOptions = new ()
        {
            PropertyNameCaseInsensitive = true,
        };

        public static readonly JsonSerializerOptions SerializerOptions = new ()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public static void Dump(this WeatherInfo wi)
        {
            if (wi == null)
            {
                return;
            }

            Console.WriteLine($"Id={wi.Id}");

            Console.WriteLine($"Сегодня:");
            wi.Current.Dump();

            Console.WriteLine($"На 5 дней:");
            foreach(var i in wi.Forecast)
            {
                i.Dump();
            }
        }

        public static void Dump(this WeatherDaily wd)
        {
            if (wd == null)
            {
                return;
            }

            Console.WriteLine($"{wd.Date} Условия={wd.Condition} T={wd.Temp}(C) Tmin={wd.TempMin}(С) Tmax={wd.TempMax}(С) Ветер={wd.WindSpeed}(м/с) {wd.WindDirection} Осадки={wd.PoPValue}");
        }

        public static void Dump(this GeoLocation gl)
        {
            if (gl == null)
            {
                return;
            }

            Console.WriteLine($"Место Id={gl.Id}");
            Console.WriteLine($"Широта={gl.Point.Lat} Долгота={gl.Point.Lon} Страна={gl.Country} Название={gl.Name} Др.названия={gl.AltNamesCsv}");
        }
    }
}
