using WeatherDemo.Domain.Entities;
using WeatherDemo.WebApi.Client.Models;

namespace WeatherDemo.WebApi.Mapping
{
    internal static class ImageInfoMapper
    {
        public static GeoLocationImageDto ToDto(this GeoLocationImage loc)
        {
            if (loc == null)
            {
                return null;
            }


            var dto = new GeoLocationImageDto()
            {
                Id = loc.Id,
                Name = loc.Name,
                Latitude = loc.Point.Lat,
                Longitude = loc.Point.Lon,
                Url= loc.Url,
            };

            return dto;
        }
    }
}
