using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Feature.LeaveRequest.Shared;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestValidator : AbstractValidator<CreateLeaveRequestCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public CreateLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;
		Include(new BaseLeaveRequestValidator(leaveTypeRepository));
	}
}
