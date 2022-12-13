using WeatherDemo.Domain.Abstractions;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Domain.Entities
{
    public class GeoLocation : IEntity
    {
        public Guid Id { get; set; }

        public GeoPoint Point { get; set; }

        public string Name { get; set; } = string.Empty;

        public string AltNamesCsv { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public DateTime Created { get; set; }
    }
}
