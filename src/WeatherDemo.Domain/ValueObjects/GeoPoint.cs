using System.Globalization;
using WeatherDemo.Domain.Exeptions;

namespace WeatherDemo.Domain.ValueObjects
{
    /// <summary>
    /// https://nietras.com/2021/06/14/csharp-10-record-struct/
    /// </summary>
    public record GeoPoint
    {
        private const string geoFormat = "00.0000000";

        public decimal Lat { get; init; }
        public decimal Lon { get; init; }

        public GeoPoint(decimal lat, decimal lon)
        {
            Lat = decimal.Round(lat, 7);
            Lon = decimal.Round(lon, 7);
        }

        public (string, string) Format()
        {
            var lat = Lat.ToString(geoFormat, CultureInfo.InvariantCulture);
            var lon = Lon.ToString(geoFormat, CultureInfo.InvariantCulture);

            return (lat, lon);
        }

        public void Validate()
        {
            if (Math.Abs(Lat) > 90 || Math.Abs(Lon) > 180)
            {
                throw new BadRequestException($"Invalid GeoPoint={Format()}");
            }
        }
    }
}
