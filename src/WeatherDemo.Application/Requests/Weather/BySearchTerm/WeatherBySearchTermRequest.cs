using WeatherDemo.Domain.Exeptions;

namespace WeatherDemo.Application.Requests.Weather.BySearchTerm
{
    public class WeatherBySearchTermRequest : WeatherRequestBase
    {
        public string Term { get; init; }

        public WeatherBySearchTermRequest(string term)
        {
            Validate(term);
            Term = term;
        }

        private void Validate(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new BadRequestException($"Invalid search term.");
            }
        }
    }
}
