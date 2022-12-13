namespace WeatherDemo.Domain.Shared.Enums
{
    public enum WeatherCondition : int
    {
        Unknown = 0,
        Clear = 100,
        FewClouds = 200,
        ScatteredClouds = 210,
        BrokenClouds = 220,
        Rain = 300,
        LightRain = 320,
        ShowerRain = 330,
        Thunderstorm = 400,
        HeavyThunderstorm = 420,
        Snow = 500,
        HeavySnow = 520,
        Mist = 600,
        Squall = 700,
    }
}
