using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherDemo.Application.Requests.Location;
using WeatherDemo.WebApi.Client.Models;
using WeatherDemo.WebApi.Mapping;

namespace WeatherDemo.WebApi.Controllers
{
    [ApiController]
    [Route("location")]
    public class GeoLocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeoLocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(GeoLocationDto[]), 200)]
        public async Task<GeoLocationDto[]> GetList([FromQuery] int skip, [FromQuery] int take)
        {
            var request = new LocationListRequest { Skip = skip, Take = take > 0 ? take : 20 };
            var response = await _mediator.Send(request);
            var dtos = response.Select(d => d.ToDto()).ToArray();

            return dtos!;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GeoLocationDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetItem(Guid id)
        {
            var request = new LocationItemRequest { Id = id };
            var response = await _mediator.Send(request);
            var dto = response.ToDto();

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var request = new LocationDeleteRequest { Id = id };
            var deleted = await _mediator.Send(request);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
