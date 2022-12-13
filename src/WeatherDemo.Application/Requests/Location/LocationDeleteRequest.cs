using MediatR;

namespace WeatherDemo.Application.Requests.Location
{
    public class LocationDeleteRequest : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
