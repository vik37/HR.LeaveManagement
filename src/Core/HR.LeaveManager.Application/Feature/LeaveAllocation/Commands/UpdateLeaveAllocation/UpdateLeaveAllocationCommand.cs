﻿using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public int NumberOfDays { get; set; }
	public int LeaveTypeId { get; set; }
	public int Period { get; set; }
}
