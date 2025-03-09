using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest
{
	public int Id { get; set; }
}
