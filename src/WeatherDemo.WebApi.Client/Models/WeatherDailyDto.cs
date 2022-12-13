namespace WeatherDemo.WebApi.Client.Models
{
    public record WeatherDailyDto
    {
        public string DayWithMonth { get; init; }

        public string DayOfWeek { get; init; }

        public string TempMax { get; set; }

        public string TempMin { get; set; }

        public string WindSpeed { get; init; }

        public string WindDirection { get; init; }

        public string WeatherCondition { get; init; }

        public string ConditionIconName { get; init; }

        public string PoP { get; init; }
    }
}
