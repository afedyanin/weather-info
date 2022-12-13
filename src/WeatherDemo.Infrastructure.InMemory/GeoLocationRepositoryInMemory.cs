using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;

[assembly: InternalsVisibleTo("WeatherDemo.Application.Tests")]

namespace WeatherDemo.Infrastructure.InMemory
{
    internal class GeoLocationRepositoryInMemory : IGeoLocationRepository
    {
        private const int MinTermLength = 4;

        private readonly ConcurrentDictionary<Guid, GeoLocation> _locations = new ConcurrentDictionary<Guid, GeoLocation>();

        public GeoLocation[] Find(string term)
        {
            if (string.IsNullOrEmpty(term) || term.Length < MinTermLength)
            {
                return Array.Empty<GeoLocation>();
            }

            var exact = _locations.Values
                .Where(loc => string.Compare(loc.Name, term, StringComparison.InvariantCultureIgnoreCase) == 0)
                .ToArray();

            if (exact.Length > 0)
            {
                return exact;
            }

            var prox = _locations.Values
                .Where(loc => loc.AltNamesCsv.Contains(term, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();

            return prox;
        }

        public GeoLocation GetByGeoPoint(GeoPoint point)
        {
            var res = _locations.Values
                .Where(loc => loc.Point == point).FirstOrDefault();

            return res;
        }

        public GeoLocation GetById(Guid id)
        {
            _locations.TryGetValue(id, out var res);
            return res;
        }

        public GeoLocation[] GetList(int skip, int take)
        {
            return _locations.Values.Skip(skip).Take(take).ToArray();
        }

        public bool Save(GeoLocation item)
        {
            if (item == null)
            {
                return false;
            }

            var res = _locations.AddOrUpdate(item.Id, item, (key, old) => item);

            return true;
        }
        public bool Delete(Guid Id)
        {
            var res = _locations.TryRemove(Id, out var _);
            return res;
        }
    }
}
