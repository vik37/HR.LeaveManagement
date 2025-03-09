using HR.LeaveManager.Application.Feature.LeaveRequest.Shared;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest
{
	public int Id { get; set; }
	public string RequestComments { get; set; } = string.Empty;
}
