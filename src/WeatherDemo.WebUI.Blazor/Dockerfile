FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WeatherDemo.WebApi.Client/WeatherDemo.WebApi.Client.csproj", "src/WeatherDemo.WebApi.Client/"]
COPY ["src/WeatherDemo.WebUI.Blazor/WeatherDemo.WebUI.Blazor.csproj", "src/WeatherDemo.WebUI.Blazor/"]

RUN dotnet restore "src/WeatherDemo.WebUI.Blazor/WeatherDemo.WebUI.Blazor.csproj"

COPY . .

WORKDIR "src/WeatherDemo.WebUI.Blazor"
RUN dotnet build "WeatherDemo.WebUI.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherDemo.WebUI.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/WeatherDemo.WebUI.Blazor/nginx.conf /etc/nginx/nginx.conf

