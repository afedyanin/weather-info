using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace WeatherDemo.Infrastructure.Data.SQLite
{
    public class WeatherDemoDbContextMigrations : IDesignTimeDbContextFactory<WeatherDemoDbContext>
    {
        private const string DbPath = "Data Source=WeatherDemo.db";

        public WeatherDemoDbContext CreateDbContext(string[] args)
        {
            var options = Options.Create(new WeatherDemoDbContextOptions()
            {
                ConnectionString = DbPath,
            });

            return new WeatherDemoDbContext(options);
        }
    }
}
