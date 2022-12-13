using MediatR;
using WeatherDemo.Domain.Entities;

namespace WeatherDemo.Application.Requests.Location
{
    public class LocationItemRequest : IRequest<GeoLocation>
    {
        public Guid Id { get; init; }
    }
}
