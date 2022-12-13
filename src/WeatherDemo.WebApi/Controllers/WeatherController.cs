using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherDemo.Application.Requests.Weather.ByGeoPoint;
using WeatherDemo.Application.Requests.Weather.BySearchTerm;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.WebApi.Client.Models;
using WeatherDemo.WebApi.Mapping;

namespace WeatherDemo.WebApi.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("point")]
        [ProducesResponseType(typeof(WeatherInfoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetForLocation([FromQuery] decimal lat, [FromQuery] decimal lon)
        {
            var request = new WeatherByGeoPointRequest(new GeoPoint(lat, lon));
            var response = await _mediator.Send(request);

            var dto = response.ToDto();

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(WeatherInfoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetForQuery([FromQuery] string query)
        {
            var request = new WeatherBySearchTermRequest(query);
            var response = await _mediator.Send(request);
            var dto = response.ToDto();

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }
    }
}
