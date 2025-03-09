using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;

	public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository,
		ILeaveAllocationRepository leaveAllocationRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveAllocationRepository = leaveAllocationRepository;

		RuleFor(p => p.NumberOfDays)
			.GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

		RuleFor(p => p.Period)
			.GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

		RuleFor(p => p.LeaveTypeId)
			.GreaterThan(0)
			.MustAsync(LeaveTypeMustExist)
			.WithMessage("{PropertyName} does not exist.");

		RuleFor(p => p.Id)
			.GreaterThan(0)
			.MustAsync(LeaveAllocationMustExist)
			.WithMessage("{PropertyName} does not exist.");

		RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
	}

	private async Task<bool> LeaveTypeMustExist(int id, CancellationToken ctn)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
		return leaveType is not null;
	}

	private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken ctn)
	{
		var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
		return leaveAllocation is not null;
	}
}
