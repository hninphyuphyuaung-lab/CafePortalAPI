using MediatR;
using Microsoft.AspNetCore.Mvc;
using CafePortalAPI.Application.Commands.Cafe;
using CafePortalAPI.Application.Queries;
using CafePortalAPI.Application.Models;

namespace CafePortalAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CafesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? location)
        {
            var query = new GetCafesQuery(location);
            var cafes = await _mediator.Send(query);
            return Ok(cafes);
        }

        [HttpPost]
        public async Task<ActionResult<CafeDto>> Post([FromBody] CreateCafeCommand request)
        {
            if (request == null)
            {
                return BadRequest("Cafe data is required.");
            }
            var cafe = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { id = cafe.Id }, cafe);
        }

        [HttpPut("cafe")]
        public async Task<IActionResult> UpdateCafe([FromBody] UpdateCafeCommand request)
        {
            if (request == null)
            {
                return BadRequest("Cafe data is required.");
            }
            var updatedCafe = await _mediator.Send(request);
            return Ok(updatedCafe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var command = new DeleteCafeCommand(id);
            await _mediator.Send(command);
            return NoContent(); 
        }

    }
}
