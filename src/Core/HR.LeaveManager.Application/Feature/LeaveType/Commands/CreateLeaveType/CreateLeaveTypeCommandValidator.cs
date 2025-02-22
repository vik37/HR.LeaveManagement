using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
	{
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

	private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
		=> _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
}
