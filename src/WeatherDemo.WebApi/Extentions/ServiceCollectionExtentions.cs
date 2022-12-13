using System.Net;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using WeatherDemo.Domain.Exeptions;

namespace WeatherDemo.WebApi.Extentions
{
    internal static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddProblemDetailsExt(this IServiceCollection services)
        {
            return services
                .AddProblemDetails(setup =>
                {
                    setup.IncludeExceptionDetails = (ctx, env) => false;

                    setup.Map<ApiException>(ex => new ProblemDetails
                    {
                        Title = ex.Message,
                        Detail = ex.Content,
                        Status = (int)ex.StatusCode,
                    });

                    setup.Map<BadRequestException>(ex => new ProblemDetails
                    {
                        Title = "Bad request",
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.BadRequest,
                    });
                });
        }
    }
}
