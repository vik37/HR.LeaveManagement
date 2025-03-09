using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;
		RuleFor(p => p.LeaveTypeId)
			.GreaterThan(0)
			.MustAsync(LeaveTypeMustExist)
			.WithMessage("{PropertyName} must exist.");
	}

	private async Task<bool> LeaveTypeMustExist(int id, CancellationToken ctn)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
		return leaveType is not null;
	}
}
