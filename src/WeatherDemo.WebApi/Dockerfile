FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/WeatherDemo.WebApi/WeatherDemo.WebApi.csproj", "src/WeatherDemo.WebApi/"]
COPY ["src/WeatherDemo.Application/WeatherDemo.Application.csproj", "src/WeatherDemo.Application/"]
COPY ["src/WeatherDemo.External.OpenWeatherMap/WeatherDemo.External.OpenWeatherMap.csproj", "src/WeatherDemo.External.OpenWeatherMap/"]
COPY ["src/WeatherDemo.Domain/WeatherDemo.Domain.csproj", "src/WeatherDemo.Domain/"]
COPY ["src/WeatherDemo.Domain.Shared/WeatherDemo.Domain.Shared.csproj", "src/WeatherDemo.Domain.Shared/"]
COPY ["src/WeatherDemo.Infrastructure.Data.SQLite/WeatherDemo.Infrastructure.Data.SQLite.csproj", "src/WeatherDemo.Infrastructure.Data.SQLite/"]
COPY ["src/WeatherDemo.Infrastructure.InMemory/WeatherDemo.Infrastructure.InMemory.csproj", "src/WeatherDemo.Infrastructure.InMemory/"]
COPY ["src/WeatherDemo.WebApi.Client/WeatherDemo.WebApi.Client.csproj", "src/WeatherDemo.WebApi.Client/"]

RUN dotnet restore "src/WeatherDemo.WebApi/WeatherDemo.WebApi.csproj"

COPY . .

WORKDIR "src/WeatherDemo.WebApi"
RUN dotnet build "WeatherDemo.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherDemo.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherDemo.WebApi.dll", "--urls", "http://*:80"]
