using System.Globalization;
using RestEase;
using WeatherDemo.External.OpenWeatherMap.Tests;

namespace WeatherDemo.WebApi.Client.Tests;

public class ClientTests
{
    [Test]
    public async Task CanFetchDataFromClient()
    {
        var client = RestClient.For<IWeatherDemoApiClient>("https://localhost:7122");

        var res = await client.GetForLocation(TH.Moscow.Lat, TH.Moscow.Lon);

        Assert.That(res, Is.Not.Null);
    }

    [Test]
    public void CanFormatDateTime()
    {
        var dt = DateTime.UtcNow;
        var date = DateOnly.FromDateTime(dt);

        CultureInfo culture = new("ru-Ru");

        var dayOfWeekName = date.ToString("dddd", culture);
        var dayWithMonthName = date.ToString("M", culture);

        Console.WriteLine($"{ dayOfWeekName } {dayWithMonthName}");
    }
}

