namespace WeatherDemo.Infrastructure.Data.SQLite
{
    public class WeatherDemoDbContextOptions
    {
        public static readonly string SectionName = "SqliteDbContext";
        public string ConnectionString { get; set; } = string.Empty;
    }
}
