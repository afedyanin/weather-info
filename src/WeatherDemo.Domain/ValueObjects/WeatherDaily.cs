using WeatherDemo.Domain.Shared.Enums;

namespace WeatherDemo.Domain.ValueObjects
{
    public class WeatherDaily
    {
        public DateOnly Date { get; set; }

        public WeatherCondition Condition { get; set; }

        public decimal Temp { get; set; }

        public decimal TempMax { get; set; }

        public decimal TempMin { get; set; }

        public decimal WindSpeed { get; set; }

        public WindDirection WindDirection { get; set; }

        // Probability of precipitation - Вероятность осадков 
        public decimal? PoP { get; set; }

        public string PoPValue
        {
            get
            {
                var prob = (int)((PoP ?? -1) * 100);
                return prob >= 0 ? prob.ToString() : string.Empty;
            }
        }
    }
}
