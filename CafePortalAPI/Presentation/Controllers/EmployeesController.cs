using MediatR;
using Microsoft.AspNetCore.Mvc;
using CafePortalAPI.Application.Queries;
using CafePortalAPI.Application.Models;
using CafePortalAPI.Application.Commands.Cafe;
using CafePortalAPI.Application.Commands.Employee;

namespace CafePortalAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? cafe)
        {
            var query = new GetEmployeesQuery(cafe);
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody] CreateEmployeeCommand command)
        {
            var cafe = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = cafe.Id }, cafe);
        }

        [HttpPut("employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand request)
        {
            if (request == null)
            {
                return BadRequest("Employee data is required.");
            }
            var updatedEmployee = await _mediator.Send(request);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
