using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherDemo.Application
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddApplcationRequestHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

            return serviceCollection;
        }
    }
}
