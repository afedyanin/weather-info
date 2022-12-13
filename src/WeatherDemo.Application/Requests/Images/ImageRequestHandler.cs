using MediatR;
using WeatherDemo.Domain.Entities;

namespace WeatherDemo.Application.Requests.Images
{
    public class ImageRequestHandler : IRequestHandler<ImageByGeoPointRequest, GeoLocationImage>
    {
        public Task<GeoLocationImage> Handle(ImageByGeoPointRequest request, CancellationToken cancellationToken)
        {
            // TODO: Implement this
            // 0 - Get image info from DB. If not found: 
            // 1 - Call external service to get images by location
            // 2 - Save image info in DB

            var res = new GeoLocationImage()
            {
                Url = "images/Moscow@2x.jpg",
            };

            return Task.FromResult(res);
        }
    }
}
