using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<int>
{
	public int LeaveTypeId { get; set; }
}
