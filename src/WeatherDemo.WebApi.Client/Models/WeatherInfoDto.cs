namespace WeatherDemo.WebApi.Client.Models
{
    public record WeatherInfoDto
    {
        public Guid Id { get; init; }

        public GeoLocationDto GeoLocation { get; init; }

        public string TimeStamp { get; init; }

        public string DayWithMonth { get; init; }

        public string DayOfWeek { get; init; }

        public string Temp { get; init; }

        public string WindSpeed { get; init; }

        public string WindDirection { get; init; }

        public string WeatherCondition { get; init; }

        public string ConditionIconName { get; init; }

        public string PoP { get; init; }

        public WeatherDailyDto[] Days { get; init; }
    }
}
