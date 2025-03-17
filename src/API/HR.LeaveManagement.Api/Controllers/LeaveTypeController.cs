using HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

		public LeaveTypeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/<LeaveTypeController>
		[HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
            => await _mediator.Send(new GetLeaveTypesQuery());

        // GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        public async Task<LeaveTypeDetailsDto> Get(int id)
           => await _mediator.Send(new GetLeaveTypeDetailsQuery(id));

        // POST api/<LeaveTypeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody]CreateLeaveTypeCommand command)
        {
           var response = await _mediator.Send(command, default);
            return CreatedAtAction(nameof(Get), new {id = response});
        }

        // PUT api/<LeaveTypeController>/5
        [HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveTypeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommaind() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
