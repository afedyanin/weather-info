using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.External.OpenWeatherMap.Tests;
using WeatherDemo.Infrastructure.InMemory;

namespace WeatherDemo.Infrastructure.Tests
{
    public class WeatherInfoCacheTests
    {
        private IWeatherInfoCache _cache;

        [SetUp]
        public void SetUp()
        {
            var inner = new MemoryCache(TH.MemoryCacheOptions);
            _cache = new WeatherInfoCache(TH.WeatherInfoCacheOptions, NullLogger<WeatherInfoCache>.Instance);
        }

        [Test]
        public void CanUseWeatherInfoCache()
        {
            var item = new WeatherInfo()
            {
                Point = TH.Moscow,
                Current = new WeatherDaily()
                {
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                }
            };

            _cache.Set(item);
            Task.Delay(300).Wait();

            var res = _cache.Get(TH.Moscow, DateTime.UtcNow);
            Assert.That(res, Is.Not.Null);


            Task.Delay(TH.WeatherInfoCacheOptions.Value.ExpirationSeconds * 1000).Wait();

            var expired = _cache.Get(TH.Moscow, DateTime.UtcNow);
            Assert.That(expired, Is.Null);
        }
    }
}
