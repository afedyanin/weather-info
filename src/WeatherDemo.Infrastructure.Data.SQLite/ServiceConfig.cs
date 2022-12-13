using Microsoft.Extensions.DependencyInjection;
using WeatherDemo.Domain.Repositories;

namespace WeatherDemo.Infrastructure.Data.SQLite
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddSQLiteGeoLocationRepository(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<WeatherDemoDbContext>();
            serviceCollection.AddScoped<IGeoLocationRepository, GeoLocationRepository>();

            return serviceCollection;
        }
    }
}
