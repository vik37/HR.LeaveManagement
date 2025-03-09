using FluentValidation;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
	public ChangeLeaveRequestApprovalValidator()
	{
		RuleFor(p => p.Approved)
			.NotNull()
			.WithMessage("Approval status can not be null.");
	}
}
