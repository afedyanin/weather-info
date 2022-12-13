using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherDemo.Application.Requests.Images;
using WeatherDemo.Domain.ValueObjects;
using WeatherDemo.WebApi.Client.Models;
using WeatherDemo.WebApi.Mapping;

namespace WeatherDemo.WebApi.Controllers
{
    [ApiController]
    [Route("images")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("point")]
        [ProducesResponseType(typeof(GeoLocationImageDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetForLocation([FromQuery] decimal lat, [FromQuery] decimal lon)
        {
            var pos = new GeoPoint(lat, lon);
            var request = new ImageByGeoPointRequest(pos);

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
