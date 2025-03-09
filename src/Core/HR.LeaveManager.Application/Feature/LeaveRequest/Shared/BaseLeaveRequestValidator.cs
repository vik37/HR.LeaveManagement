using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;

		RuleFor(p => p.StartDate)
			.LessThan(p => p.EndDate)
			.WithMessage("{PropertyName} must be before {ComparasionValue}");

		RuleFor(p => p.EndDate)
			.GreaterThan(p => p.StartDate)
			.WithMessage("{PropertyName} must be after {ComparasionValue}");

		RuleFor(p => p.LeaveTypeId)
			.GreaterThan(0)
			.MustAsync(LeaveTypeMustExists)
			.WithMessage("{PropertyName} does not exist.");
	}

	public async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
		return leaveType is not null;
	}
}
