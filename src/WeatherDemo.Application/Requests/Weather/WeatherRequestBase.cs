using MediatR;

namespace WeatherDemo.Application.Requests.Weather
{
    public abstract class WeatherRequestBase : IRequest<WeatherResponse>
    {
        public DateTime TimeStamp { get; init; }

        protected WeatherRequestBase(DateTime? timeStamp = null)
        {
            TimeStamp = timeStamp ?? DateTime.UtcNow;
        }
    }
}
