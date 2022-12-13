using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WeatherDemo.WebApi.Client;
using WeatherDemo.WebApi.Client.Models;

namespace WeatherDemo.WebUI.Blazor.Pages
{
    public partial class Index
    {
        private record Position
        {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        };

        private record SearchFormModel
        {
            public string SearchText { get; set; } = string.Empty;
        };

        private static readonly Position _defaultPositionMoscow = new()
        {
            Latitude = 55.7504461M,
            Longitude = 37.6174943M
        };

        private static readonly GeoLocationImageDto _defaultImageMoscow = new()
        {
            Url = "images/Moscow@2x.jpg"
        };

        private static readonly string[] _images =
        {
            "images/bnr01.png",
            "images/bnr02.png",
            "images/bnr03.png",
            "images/bnr04.png",
            "images/bnr05.png",
            "images/bnr06.png",
        };

        private static readonly Random _random = new Random();

        [Inject] private IWeatherDemoApiClient? _client { get; set; }

        [Inject] private IJSRuntime? _js { get; set; } 

        private WeatherInfoDto? _weather;
        private string _message = string.Empty;
        private SearchFormModel _searchForm = new ();
        private Position? _position;
        private GeoLocationImageDto? _imageDto = _defaultImageMoscow;


        protected override async Task OnInitializedAsync()
        {
            await TryDetectGeoPosition();
            await UpdateWeatherInfo();
        }

        private async Task UpdateWeatherInfo()
        {
            var pos = _position ?? _defaultPositionMoscow;

            try
            {
                _weather = await _client!.GetForLocation(pos.Latitude, pos.Longitude);
                _imageDto = await GetImage(pos.Latitude, pos.Longitude);
            }
            catch (Exception ex)
            {
                _message = $"Error: {ex.Message}";
            }
        }

        private async Task OnSearchSubmit()
        {
            try
            {
                var term = _searchForm.SearchText;

                if (string.IsNullOrEmpty(term) || term.Length < 4)
                {
                    return;
                }

                _weather = await _client!.GetForQuery(term);
                _message = $"Результат поиска. Локация: {_weather.GeoLocation.Name}";
                _searchForm.SearchText = string.Empty;
                _imageDto = await GetImage(_weather.GeoLocation.Lat, _weather.GeoLocation.Lon);
            }
            catch (Exception ex)
            {
                _message = $"Error: {ex.Message}";
            }
        }

        private async Task TryDetectGeoPosition()
        {
            try
            {
                _position = await _js!.InvokeAsync<Position>("cbInterop.getPosition");
                _message = $"Позиция определена: {_position.Latitude} {_position.Longitude}";
            }
            catch
            {
                _message = "Geolocation is not supported.";
            }
        }

        private Task<GeoLocationImageDto> GetImage(decimal lat, decimal lon)
        {
            var pos = new Position
            {
                Latitude = lat,
                Longitude = lon
            };

            if (pos == _defaultPositionMoscow)
            {
                return Task.FromResult(_defaultImageMoscow);
            }

            var rndStub = new GeoLocationImageDto
            {
                Id = Guid.NewGuid(),
                Latitude = lat,
                Longitude = lon,
                Name = "Random",
                Url = _images[_random.Next(0, _images.Length)]
            };

            return Task.FromResult(rndStub);

            // TODO: Implement this
            // return await _client!.FindImage(pos.Latitude, pos.Longitude);
        }
    }
}
