using Microsoft.Extensions.DependencyInjection;
using WeatherDemo.Domain.Repositories;

namespace WeatherDemo.Infrastructure.InMemory
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddWeatherInfoCache(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWeatherInfoCache, WeatherInfoCache>();
            return serviceCollection;
        }

        public static IServiceCollection AddInMemoryGeoLocationRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGeoLocationRepository, GeoLocationRepositoryInMemory>();
            return serviceCollection;
        }
    }
}
