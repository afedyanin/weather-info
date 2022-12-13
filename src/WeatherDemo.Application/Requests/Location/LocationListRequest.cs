using MediatR;
using WeatherDemo.Domain.Entities;

namespace WeatherDemo.Application.Requests.Location
{
    public class LocationListRequest : IRequest<GeoLocation[]>
    {
        public int Skip { get; init; }

        public int Take { get; init; }
    }
}
