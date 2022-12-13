using MediatR;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Application.Requests.Images
{
    public class ImageByGeoPointRequest : IRequest<GeoLocationImage>
    {
        public GeoPoint GeoPoint { get; init; }

        public ImageByGeoPointRequest(GeoPoint geoPoint)
        {
            geoPoint.Validate();
            GeoPoint = geoPoint;
        }
    }
}
