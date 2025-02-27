using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
	{
		RuleFor(x => x.Id)
			.NotNull()
			.MustAsync(LeaveTypeMustExists);

		RuleFor(x => x.Name)
			.Empty().WithMessage("{PropertyName} is required")
			.NotNull()
			.MaximumLength(70).WithMessage("{PropertyName}  must be fewer than 70 characters");

		RuleFor(x => x.DefaultDays)
			.GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
			.LessThan(1).WithMessage("{PropertyName} cannot be less than 1");

		RuleFor(q => q)
			.MustAsync(LeaveTypeNameUnique)
			.WithMessage("Leave Type allready exist");

		_leaveTypeRepository = leaveTypeRepository;
	}

	public async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
		=> await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);

	public async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
		return leaveType is not null;
	}
}
