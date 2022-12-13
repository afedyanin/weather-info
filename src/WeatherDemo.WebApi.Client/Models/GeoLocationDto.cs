namespace WeatherDemo.WebApi.Client.Models
{
    public record GeoLocationDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Lat { get; init; }

        public decimal Lon { get; init; }

        public string AltNames { get; init; }

        public string Country { get; init; }
    }
}
