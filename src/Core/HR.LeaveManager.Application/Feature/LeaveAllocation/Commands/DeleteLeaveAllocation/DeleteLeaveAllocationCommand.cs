using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommand : IRequest
{
	public int Id { get; set; }
}
