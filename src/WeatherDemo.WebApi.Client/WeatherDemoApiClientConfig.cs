using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;

namespace WeatherDemo.WebApi.Client
{
    public static class WeatherDemoApiClientConfig
    {
        public static IHttpClientBuilder AddWeatherDemoApiClient(this IServiceCollection services, string baseAddress)
        {
            return services.AddRestEaseClient<IWeatherDemoApiClient>(baseAddress);
        }
    }
}
