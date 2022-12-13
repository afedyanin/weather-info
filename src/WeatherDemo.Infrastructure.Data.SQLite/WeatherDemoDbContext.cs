using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Infrastructure.Data.SQLite.EntitiesConfiguration;

namespace WeatherDemo.Infrastructure.Data.SQLite
{
    public class WeatherDemoDbContext : DbContext
    {
        private readonly IOptions<WeatherDemoDbContextOptions> _options;

        public DbSet<GeoLocation>? Locations { get; set; }

        public WeatherDemoDbContext(IOptions<WeatherDemoDbContextOptions> options)
        {
            ArgumentNullException.ThrowIfNull(nameof(options));

            if (string.IsNullOrEmpty(options.Value.ConnectionString))
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfiguration(new GeoLocationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_options.Value.ConnectionString);
    }
}
