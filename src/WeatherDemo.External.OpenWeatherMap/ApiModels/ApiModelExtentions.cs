using FluentAssertions;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Shared.Enums;
using WeatherDemo.Domain.Shared.Utils;
using WeatherDemo.Domain.ValueObjects;

namespace WeatherDemo.External.OpenWeatherMap.ApiModels
{
    internal static class ApiModelExtentions
    {
        public static GeoLocation? ToGeoLocation(this GeoData geo)
        {
            if (geo == null)
            {
                return null;
            }

            var name = string.IsNullOrEmpty(geo.Local_names.Ru) ? geo.Name : geo.Local_names.Ru;

            var res = new GeoLocation()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Point = new GeoPoint(geo.Lat, geo.Lon),
                Country = geo.Country,
                Name = name,
                AltNamesCsv = string.Join(',', geo.Local_names.ToAltNames(geo.Name)),
            };

            return res;
        }

        public static WeatherInfo ToWeatherInfo(this CurrentWeather current, Forecast5Days? forecast, GeoPoint point)
        {
            current.Should().NotBeNull();
            forecast.Should().NotBeNull();

            var dailyForecast = forecast!.ToWeatherDaily();

            var res = new WeatherInfo()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Point = point,
                Current = current.ToWeatherDaily(dailyForecast.First()),
                Forecast = dailyForecast,
            };

            return res;
        }

        public static WeatherDaily[] ToWeatherDaily(this Forecast5Days forecast)
        {
            forecast.Should().NotBeNull();
            forecast.List.Should().NotBeNull();

            var items = forecast.List
                .GroupBy(k => UnixTime.UnixSecondsToDateTime(k.Dt).Day)
                .Select(g => new
                {
                    First = g.OrderBy(d => d.Dt).First(),
                    MaxTemp = g.Max(d => d.Main.Temp_max),
                    MinTemp = g.Min(d => d.Main.Temp_min),
                    MaxPop = g.Max(d => d.Pop)
                });

            var res = items.Select(i => new WeatherDaily
            {
                Date = DateOnly.FromDateTime(UnixTime.UnixSecondsToDateTime(i.First.Dt)),
                Condition = i.First.Weather.First().ToWeatherCondition(),
                WindDirection = i.First.Wind.ToWindDirection(),
                WindSpeed = i.First.Wind.Speed,
                PoP = i.MaxPop,
                Temp = TempConverter.K2C(i.First.Main.Temp),
                TempMax = TempConverter.K2C(i.MaxTemp),
                TempMin = TempConverter.K2C(i.MinTemp),
            }) ;

            return res.ToArray();
        }

        public static string[] ToAltNames(this LocalNames names, string main)
        {
            var res = new List<string>()
            {
                main,
                names.Ru,
                names.En,
                names.Ascii,
                names.Feature_name
            };

            return res.Distinct().ToArray();
        }

        public static WeatherDaily ToWeatherDaily(this CurrentWeather current, WeatherDaily? forecastToday = null)
        {
            current.Should().NotBeNull();

            var res = new WeatherDaily
            {
                Date = DateOnly.FromDateTime(UnixTime.UnixSecondsToDateTime(current.Dt)),
                Condition = current.Weather.First().ToWeatherCondition(),
                WindDirection = current.Wind.ToWindDirection(),
                WindSpeed = current.Wind.Speed,
                Temp = TempConverter.K2C(current.Main.Temp),
                TempMax = TempConverter.K2C(current.Main.Temp_max),
                TempMin = TempConverter.K2C(current.Main.Temp_min),
                PoP = forecastToday?.PoP,
            };

            return res;
        }

        public static WindDirection ToWindDirection(this Wind wind)
        {
            if (wind == null)
            {
                return WindDirection.Unknown;
            }

            // https://openweathermap.org/current
            var res = wind.Deg switch
            {
                >= 0 and <= 23 => WindDirection.North,
                > 23 and <= 45 => WindDirection.NorthEast,
                > 45 and <= 68 => WindDirection.NorthEast,
                > 68 and <= 90 => WindDirection.East,
                > 90 and <= 113 => WindDirection.East,
                > 113 and <= 135 => WindDirection.SouthEast,
                > 135 and <= 158 => WindDirection.SouthEast,
                > 158 and <= 180 => WindDirection.South,
                > 180 and <= 203 => WindDirection.South,
                > 203 and <= 225 => WindDirection.SouthWest,
                > 225 and <= 248 => WindDirection.SouthWest,
                > 248 and <= 270 => WindDirection.West,
                > 270 and <= 293 => WindDirection.West,
                > 293 and <= 315 => WindDirection.NorthWest,
                > 315 and <= 338 => WindDirection.NorthWest,
                > 338 and <= 360 => WindDirection.North,
                _ => WindDirection.Unknown
            };

            return res;
        }

        public static WeatherCondition ToWeatherCondition(this Weather wc)
        {
            if (wc == null)
            {
                return WeatherCondition.Unknown;
            }

            // https://openweathermap.org/weather-conditions
            var res = wc.Id switch
            {
                0 => WeatherCondition.Unknown,
                800 => WeatherCondition.Clear,
                801 => WeatherCondition.FewClouds,
                802 => WeatherCondition.ScatteredClouds,
                803 => WeatherCondition.BrokenClouds,
                804 => WeatherCondition.BrokenClouds,
                701 => WeatherCondition.Mist,
                711 => WeatherCondition.Mist,
                721 => WeatherCondition.Mist,
                731 => WeatherCondition.Mist,
                741 => WeatherCondition.Mist,
                751 => WeatherCondition.Squall,
                761 => WeatherCondition.Squall,
                762 => WeatherCondition.Squall,
                771 => WeatherCondition.Squall,
                781 => WeatherCondition.Squall,
                600 => WeatherCondition.Snow,
                601 => WeatherCondition.Snow,
                602 => WeatherCondition.HeavySnow,
                611 => WeatherCondition.Snow,
                612 => WeatherCondition.Snow,
                613 => WeatherCondition.Snow,
                615 => WeatherCondition.Snow,
                616 => WeatherCondition.Snow,
                620 => WeatherCondition.Snow,
                621 => WeatherCondition.HeavySnow,
                622 => WeatherCondition.HeavySnow,
                500 => WeatherCondition.Rain,
                501 => WeatherCondition.LightRain,
                502 => WeatherCondition.LightRain,
                503 => WeatherCondition.ShowerRain,
                504 => WeatherCondition.ShowerRain,
                511 => WeatherCondition.Snow,
                520 => WeatherCondition.ShowerRain,
                521 => WeatherCondition.ShowerRain,
                522 => WeatherCondition.ShowerRain,
                531 => WeatherCondition.ShowerRain,
                300 => WeatherCondition.Rain,
                301 => WeatherCondition.Rain,
                302 => WeatherCondition.Rain,
                310 => WeatherCondition.Rain,
                311 => WeatherCondition.Rain,
                312 => WeatherCondition.ShowerRain,
                313 => WeatherCondition.ShowerRain,
                314 => WeatherCondition.ShowerRain,
                321 => WeatherCondition.ShowerRain,
                200 => WeatherCondition.Thunderstorm,
                201 => WeatherCondition.Thunderstorm,
                202 => WeatherCondition.HeavyThunderstorm,
                210 => WeatherCondition.Thunderstorm,
                211 => WeatherCondition.Thunderstorm,
                212 => WeatherCondition.HeavyThunderstorm,
                221 => WeatherCondition.HeavyThunderstorm,
                230 => WeatherCondition.Thunderstorm,
                231 => WeatherCondition.Thunderstorm,
                232 => WeatherCondition.HeavyThunderstorm,
                _ => WeatherCondition.Unknown
            };

            return res;
        }
    }
}
