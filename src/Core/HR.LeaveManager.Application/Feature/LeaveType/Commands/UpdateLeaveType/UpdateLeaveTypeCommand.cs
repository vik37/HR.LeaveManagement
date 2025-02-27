using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int DefaultDays { get; set; }
}
