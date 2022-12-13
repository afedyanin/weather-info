namespace WeatherDemo.Infrastructure.InMemory
{
    public class WeatherInfoCacheOptions
    {
        public static readonly string SectionName = "WeatherInfoCache";

        public int ExpirationSeconds { get; set; }

        public long SizeLimit { get; set; }
    }
}
