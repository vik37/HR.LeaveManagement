using HR.LeaveManager.Application.Feature.LeaveRequest.Shared;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest
{
	public int Id { get; set; }
	public string RequestsComments { get; set; } = string.Empty;
	public bool Cancelled { get; set; }
}
