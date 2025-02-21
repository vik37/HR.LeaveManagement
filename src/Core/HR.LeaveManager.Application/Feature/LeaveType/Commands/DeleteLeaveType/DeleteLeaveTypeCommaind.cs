using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommaind : IRequest<Unit>
{
	public int Id { get; set; }
}
