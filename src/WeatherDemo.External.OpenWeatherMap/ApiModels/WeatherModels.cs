using System.Text.Json.Serialization;

namespace WeatherDemo.External.OpenWeatherMap.ApiModels
{
    /// <summary>
    /// https://openweathermap.org/current
    /// </summary>
    public record CurrentWeather(
        Coord Coord,
        Weather[] Weather,
        string Base,
        Main Main,
        int Visibility,
        Wind Wind,
        Clouds Clouds,
        Rain Rain,
        Snow Snow,
        long Dt,
        Sys Sys,
        long Timezone,
        int Id,
        string Name,
        int Cod
        );

    /// <summary>
    /// https://openweathermap.org/forecast5
    /// </summary>
    public record Forecast5Days(
        string Cod,
        int Message,
        int Cnt,
        ListItem[] List,
        City City,
        string Country,
        long Population,
        long Timezone,
        long Sunrise,
        long Sunset
        );

    public record Coord(
        decimal Lon,
        decimal Lat);

    public record Main(
        decimal Temp,
        decimal Feels_like,
        decimal Temp_min,
        decimal Temp_max,
        int Pressure,
        int Humidity,
        int Sea_level,
        int Grnd_level);

    public record Weather(
        int Id,
        string Main,
        string Description,
        string Icon); // https://openweathermap.org/weather-conditions#How-to-get-icon-URL

    public record Clouds(int All);

    public record Wind(
        decimal Speed,
        int Deg,
        decimal Gust);

    public record Rain(
        [property: JsonPropertyName("1h")] decimal OneHour,
        [property: JsonPropertyName("3h")] decimal ThreeHours);

    public record Snow(
        [property: JsonPropertyName("1h")] decimal OneHour,
        [property: JsonPropertyName("3h")] decimal ThreeHours);

    public record Sys(
        int Type,
        int Id,
        string Country,
        long Sunrise,
        long Sunset,
        string Pod);

    public record ListItem(
        long Dt,
        Main Main,
        Weather[] Weather,
        Clouds Clouds,
        Wind Wind,
        int Visibility,
        decimal Pop,
        Rain Rain,
        Snow Snow,
        Sys Sys,
        string Dt_txt
        );

    public record City(
        int Id,
        string Name,
        Coord Coord
        );
}
