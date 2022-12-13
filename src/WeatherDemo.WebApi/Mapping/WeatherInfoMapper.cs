using System.Globalization;
using WeatherDemo.Application.Requests.Weather;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Shared.Enums;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.WebApi.Client.Models;

namespace WeatherDemo.WebApi.Mapping
{
    internal static class WeatherInfoMapper
    {
        private static readonly CultureInfo Culture = new("ru-RU");

        public static WeatherInfoDto ToDto(this WeatherResponse resp)
        {
            var weather = resp?.WeatherInfo;
            var loc = resp?.GeoLocation;

            if (loc == null || weather == null)
            {
                return null;
            }

            var cur = weather.Current;

            var dto = new WeatherInfoDto()
            {
                Id = weather.Id,
                GeoLocation = loc.ToDto(),
                TimeStamp = weather.Created.ToString(),
                DayWithMonth = cur.Date.ToString("M", Culture),
                DayOfWeek = cur.Date.ToString("dddd", Culture),
                PoP = cur.PoPValue,
                Temp = ((int)cur.Temp).ToString(),
                WindSpeed = cur.WindSpeed.ToString(),
                WindDirection = cur.WindDirection.ToStringDirection(),
                WeatherCondition = cur.Condition.ToStringCondition(),
                ConditionIconName = cur.Condition.ToIconCondition(),
                Days = weather.Forecast.ToDto(cur.Date.Day),
            };

            return dto;
        }

        public static GeoLocationDto ToDto(this GeoLocation loc)
        {
            if (loc == null)
            {
                return null;
            }


            var dto = new GeoLocationDto()
            {
                Id = loc.Id,
                Name = loc.Name,
                Lat = loc.Point.Lat,
                Lon = loc.Point.Lon,
                AltNames = loc.AltNamesCsv,
                Country =loc.Country,
            };

            return dto;
        }

        public static WeatherDailyDto ToDto(this WeatherDaily day)
        {
            if (day == null)
            {
                return null;
            }

            var dto = new WeatherDailyDto()
            {
                DayWithMonth = day.Date.ToString("M", Culture),
                DayOfWeek = day.Date.ToString("dddd", Culture),
                PoP = day.PoPValue,
                TempMax = ((int)day.TempMax).ToString(),
                TempMin = ((int)day.TempMin).ToString(),
                WindSpeed = day.WindSpeed.ToString(),
                WindDirection = day.WindDirection.ToStringDirection(),
                WeatherCondition = day.Condition.ToStringCondition(),
                ConditionIconName = day.Condition.ToIconCondition(),
            };

            return dto;
        }

        public static WeatherDailyDto[] ToDto(this WeatherDaily[] days, int currentDay)
        {
            var dto = new List<WeatherDailyDto>();

            if (days == null)
            {
                return Array.Empty<WeatherDailyDto>();
            }

            foreach (var item in days)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.Date.Day == currentDay)
                {
                    // skip current day
                    continue;
                }

                var dtoItem = item.ToDto();

                if (dtoItem != null)
                {
                    dto.Add(dtoItem);
                }
            }

            return dto.ToArray();
        }

        public static string ToStringDirection(this WindDirection dir)
        {
            var res = dir switch
            {
                WindDirection.North => "Северный",
                WindDirection.South => "Южный",
                WindDirection.West => "Западный",
                WindDirection.East => "Южный",
                WindDirection.SouthEast => "Юго-Восточный",
                WindDirection.SouthWest => "Юго-Западный",
                WindDirection.NorthEast => "Северо-Восточный",
                WindDirection.NorthWest => "Северо-Западный",
                _ => string.Empty,
            };

            return res;
        }

        public static string ToStringCondition(this WeatherCondition con)
        {
            var res = con switch
            {
                WeatherCondition.Clear => "Ясно",
                WeatherCondition.FewClouds => "Облачно, с прояснениями",
                WeatherCondition.ScatteredClouds => "Облачно",
                WeatherCondition.BrokenClouds => "Облачно",
                WeatherCondition.Rain => "Дождь",
                WeatherCondition.LightRain => "Небольшой дождь",
                WeatherCondition.ShowerRain => "Ливень",
                WeatherCondition.Thunderstorm => "Гроза",
                WeatherCondition.HeavyThunderstorm => "Сильная гроза",
                WeatherCondition.Snow => "Снег",
                WeatherCondition.HeavySnow => "Сильный снег",
                WeatherCondition.Mist => "Туман",
                WeatherCondition.Squall => "Ураган",
                _ => string.Empty,
            };

            return res;
        }

        public static string ToIconCondition(this WeatherCondition con)
        {
            var res = con switch
            {
                WeatherCondition.Clear => "images/icons/icon-2.svg",
                WeatherCondition.FewClouds => "images/icons/icon-3.svg",
                WeatherCondition.ScatteredClouds => "images/icons/icon-5.svg",
                WeatherCondition.BrokenClouds => "images/icons/icon-6.svg",
                WeatherCondition.Rain => "images/icons/icon-9.svg",
                WeatherCondition.LightRain => "images/icons/icon-4.svg",
                WeatherCondition.ShowerRain => "images/icons/icon-10.svg",
                WeatherCondition.Thunderstorm => "images/icons/icon-12.svg",
                WeatherCondition.HeavyThunderstorm => "images/icons/icon-11.svg",
                WeatherCondition.Snow => "images/icons/icon-13.svg",
                WeatherCondition.HeavySnow => "images/icons/icon-14.svg",
                WeatherCondition.Mist => "images/icons/icon-7.svg",
                WeatherCondition.Squall => "images/icons/icon-8.svg",
                _ => "images/icons/icon-1.svg",
            };

            return res;
        }

    }
}
