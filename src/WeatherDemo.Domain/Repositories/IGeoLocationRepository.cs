using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Domain.Repositories
{
    public interface IGeoLocationRepository
    {
        GeoLocation GetById(Guid id);

        GeoLocation[] GetList(int skip, int take);

        GeoLocation GetByGeoPoint(GeoPoint point);

        GeoLocation[] Find(string term);
        bool Save(GeoLocation item);

        bool Delete(Guid Id);
    }
}
