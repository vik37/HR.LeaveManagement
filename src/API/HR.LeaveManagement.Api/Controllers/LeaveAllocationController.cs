﻿using HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
		private readonly IMediator _mediator;

		public LeaveAllocationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/<LeaveAllocationController>
		[HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());

            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
        {
            var leaveAllocationDetails = await _mediator.Send(new LeaveAllocationDelailsQuery(id));

            return Ok(leaveAllocationDetails);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { Id = response });
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> Put(int id, [FromBody]UpdateLeaveAllocationCommand command)
        {
			var response = await _mediator.Send(command);
			return NoContent();
		}

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
            return NoContent();
        }
    }
}
