using FluentValidation;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Feature.LeaveRequest.Shared;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveRequestRepository _leaveRequestRepository;

	public UpdateLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveRequestRepository = leaveRequestRepository;
		Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

		RuleFor(p => p.Id)
			.NotNull()
			.MustAsync(LeaveRequestMustExist)
			.WithMessage("{PropertyName} must be present.");
	}

	private async Task<bool> LeaveRequestMustExist(int id, CancellationToken ctn)
	{
		var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
		return leaveRequest is not null;
	}
}
