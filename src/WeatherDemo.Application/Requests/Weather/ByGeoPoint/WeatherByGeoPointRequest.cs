using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.Application.Requests.Weather.ByGeoPoint
{
    public class WeatherByGeoPointRequest : WeatherRequestBase
    {
        public GeoPoint GeoPoint { get; init; }

        public WeatherByGeoPointRequest(GeoPoint geoPoint)
        {
            geoPoint.Validate();
            GeoPoint = geoPoint;
        }
    }
}
