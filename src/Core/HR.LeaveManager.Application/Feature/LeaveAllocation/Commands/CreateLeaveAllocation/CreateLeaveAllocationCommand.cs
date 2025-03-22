using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
	public int LeaveTypeId { get; set; }
}
