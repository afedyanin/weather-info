using WeatherDemo.Domain.Abstractions;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Domain.Entities
{
    public class WeatherInfo : IEntity
    {
        public Guid Id { get; set; }

        public GeoPoint Point { get; set; }

        public WeatherDaily Current { get; set; }

        public WeatherDaily[] Forecast { get; set; }

        public DateTime Created { get; set; }
    }
}
