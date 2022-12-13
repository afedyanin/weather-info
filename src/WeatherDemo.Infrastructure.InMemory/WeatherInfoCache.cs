using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;

[assembly: InternalsVisibleTo("WeatherDemo.Infrastructure.Tests")]

namespace WeatherDemo.Infrastructure.InMemory
{
    internal class WeatherInfoCache : IWeatherInfoCache
    {
        private readonly record struct Key
        {
            public GeoPoint Point { get; init; }
            public DateOnly Date { get; init; }

            public Key(GeoPoint point, DateOnly date)
            {
                Point = point;
                Date = date;
            }

            public override string ToString()
            {
                return $"{Point.Format()}#{Date}";
            }
        }

        private const int ExpSeconds = 3 * 60;

        private readonly IMemoryCache _cache;

        private readonly MemoryCacheEntryOptions _itemOptions;

        private readonly ILogger<WeatherInfoCache> _logger;

        public WeatherInfoCache(
            IOptions<WeatherInfoCacheOptions> options,
            ILogger<WeatherInfoCache> logger)
        {
            _logger = logger;

            if (options.Value.SizeLimit == 0)
            {
                ArgumentNullException.ThrowIfNull(nameof(options.Value));
            }

            _cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = options.Value.SizeLimit
            });

            var expSeconds = options.Value.ExpirationSeconds > 0 ?
                options.Value.ExpirationSeconds : ExpSeconds;

            _itemOptions = new()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expSeconds),
            };
        }

        public WeatherInfo Get(GeoPoint point, DateTime time)
        {
            var key = new Key(point, DateOnly.FromDateTime(time));
            var res = _cache.Get<WeatherInfo>(key);

            var logMessage = res == null ?
                $"Not found cache item Key={key}" :
                $"Found cache item Key={key}";

            _logger.LogInformation(logMessage);

            return res;
        }

        public void Set(WeatherInfo item)
        {
            var key = new Key(item.Point, item.Current.Date);
            _cache.Set(key, item, _itemOptions);

            _logger.LogInformation($"Saved cache item Key={key}");
        }

        public void Remove(GeoPoint point, DateTime time)
        {
            var key = new Key(point, DateOnly.FromDateTime(time));
            _cache.Remove(key);
        }
    }
}
