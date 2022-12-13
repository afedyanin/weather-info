using WeatherDemo.Domain.Shared.Utils;

namespace WeatherDemo.External.OpenWeatherMap.Tests
{
    public class DomainSharedUtilsTests
    {
        [Test]
        public void CanConvertUnixTime()
        {
            var unixDate = 1668621600L;
            var expected = DateTime.Parse("2022-11-16 18:00:00");

            var actual = UnixTime.UnixSecondsToDateTime(unixDate);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CanFormatGeoPoint()
        {
            var (lat, lon) = TH.Moscow.Format();

            Assert.Multiple(() =>
            {
                Assert.That(lat, Is.EqualTo("55.751244"));
                Assert.That(lon, Is.EqualTo("37.618423"));
            });
        }
    }
}
