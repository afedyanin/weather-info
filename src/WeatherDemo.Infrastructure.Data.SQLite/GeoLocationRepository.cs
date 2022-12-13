using Microsoft.EntityFrameworkCore;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Repositories;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Infrastructure.Data.SQLite
{
    internal class GeoLocationRepository : IGeoLocationRepository
    {
        private readonly WeatherDemoDbContext _dbContext;
        private readonly DbSet<GeoLocation> _dbSet;

        public GeoLocationRepository(WeatherDemoDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet= _dbContext.Set<GeoLocation>();
        }

        public GeoLocation[] Find(string term)
        {
            var exactPattern = $"{term}";
            var exact = _dbSet
                .Where(loc => EF.Functions.Like(loc.Name, exactPattern))
                .ToArray();

            if (exact.Length > 0)
            {
                return exact;
            }

            var proxPattern = $"%{term}%";
            var prox = _dbSet
                .Where(loc => EF.Functions.Like(loc.AltNamesCsv, proxPattern))
                .ToArray();

            return prox;
        }

        public GeoLocation? GetByGeoPoint(GeoPoint point)
        {
            var res = _dbSet
                .Where(p =>
                    p.Point.Lat == point.Lat &&
                    p.Point.Lon == point.Lon)
                .FirstOrDefault();

            return res;
        }

        public GeoLocation? GetById(Guid id)
        {
            var res = _dbSet.Find(id);
            return res;
        }

        public GeoLocation[] GetList(int skip, int take)
        {
            return _dbSet.Skip(skip).Take(take).ToArray();
        }

        public bool Save(GeoLocation item)
        {
            _dbSet.Add(item);
            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(Guid Id)
        {
            var item = _dbSet.Find(Id);

            if (item == null)
            {
                return false;
            }

            _dbSet.Remove(item);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
