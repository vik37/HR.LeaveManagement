using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

		public LeaveRequestController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/<LeaveRequestController>
		[HttpGet]
		public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
		{
			var leaveAllocations = await _mediator.Send(new GetLeaveRequestListQuery());

			return Ok(leaveAllocations);
		}

		// GET api/<LeaveRequestController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
		{
			var leaveAllocationDetails = await _mediator.Send(new GetLeaveRequestDetailsQuery(id));

			return Ok(leaveAllocationDetails);
		}

		// POST api/<LeaveRequestController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return Ok("New Leave Request Successfully Created");
		}

		// PUT api/<LeaveRequestController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}

		// PUT api/<LeaveRequestController>/5
		[HttpPut("CancelRequest")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> CancelRequest(int id, [FromBody] CancelLeaveRequestCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}

		// PUT api/<LeaveRequestController>/5
		[HttpPut("UpdateAppruval")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> Put(int id, [FromBody] ChangeLeaveRequestApprovalCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}

		// DELETE api/<LeaveRequestController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
			return NoContent();
		}
	}
}
