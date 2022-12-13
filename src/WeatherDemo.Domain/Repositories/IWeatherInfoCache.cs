using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Domain.Repositories
{
    public interface IWeatherInfoCache 
    {
        WeatherInfo Get(GeoPoint point, DateTime time);

        void Set(WeatherInfo item);

        void Remove(GeoPoint point, DateTime time);
    }
}
