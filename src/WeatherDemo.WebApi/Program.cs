using Hellang.Middleware.ProblemDetails;
using WeatherDemo.Application;
using WeatherDemo.External.OpenWeatherMap;
using WeatherDemo.Infrastructure.Data.SQLite;
using WeatherDemo.Infrastructure.InMemory;
using WeatherDemo.WebApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection(OpenWeatherMapOptions.SectionName);
builder.Services.Configure<OpenWeatherMapOptions>(section);
var options = section.Get<OpenWeatherMapOptions>();

Console.WriteLine($"API Key from config: {options.ApiKey}");
Console.WriteLine($"Base URL from config: {options.BaseUrl}");

builder.Services.AddOpenWeatherMapService(options.BaseUrl);

builder.Services.Configure<WeatherInfoCacheOptions>(builder.
    Configuration.GetSection(WeatherInfoCacheOptions.SectionName));

builder.Services.AddWeatherInfoCache();

builder.Services.Configure<WeatherDemoDbContextOptions>(builder.
    Configuration.GetSection(WeatherDemoDbContextOptions.SectionName));

builder.Services.AddSQLiteGeoLocationRepository();
// builder.Services.AddInMemoryGeoLocationRepository();

builder.Services.AddApplcationRequestHandlers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetailsExt();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors("CorsPolicy");

// app.UseHttpsRedirection();

app.UseProblemDetails();
app.MapControllers();

/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WeatherDemoDbContext>();

    Console.WriteLine("Start Migration");
    db.Database.Migrate();
}
*/

app.Run();
