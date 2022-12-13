namespace WeatherDemo.Domain.Shared.Utils
{
    public static class TempConverter
    {
        private const decimal Kelvin = 273.15M;

        public static decimal K2C(decimal temp) => temp - Kelvin;

        public static decimal C2K(decimal temp) => temp + Kelvin;
    }
}
