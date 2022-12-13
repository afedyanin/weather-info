using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using WeatherDemo.External.OpenWeatherMap.Internal;

namespace WeatherDemo.External.OpenWeatherMap
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddOpenWeatherMapService(
            this IServiceCollection serviceCollection,
            string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl), baseUrl);
            }

            serviceCollection.AddRestEaseClient<IOpenWeatherMapClient>(baseUrl);
            serviceCollection.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();

            return serviceCollection;
        }
    }
}
