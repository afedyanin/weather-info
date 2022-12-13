namespace WeatherDemo.WebApi.Client.Models
{
    public record GeoLocationImageDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Latitude { get; init; }

        public decimal Longitude { get; init; }

        public string Url { get; init; }
    }
}
