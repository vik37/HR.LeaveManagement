using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommand : IRequest
{
	public int Id { get; set; }
}
