using WeatherDemo.Domain.Abstractions;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Domain.Entities
{
    public class GeoLocationImage : IEntity
    {
        public Guid Id { get; set; }

        public GeoPoint Point { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateTime Created { get; set; }
    }
}
