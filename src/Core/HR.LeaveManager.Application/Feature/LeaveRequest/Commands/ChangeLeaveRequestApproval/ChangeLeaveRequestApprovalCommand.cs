using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : IRequest
{
	public int Id { get; set; }
	public bool Approved { get; set; }
}
