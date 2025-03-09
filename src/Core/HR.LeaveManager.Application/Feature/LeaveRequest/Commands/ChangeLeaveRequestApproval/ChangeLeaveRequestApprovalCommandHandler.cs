using AutoMapper;
using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManager.Application.Models;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IEmailSender _emailSender;
	private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

	public ChangeLeaveRequestApprovalCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
		ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, 
		IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveRequestRepository = leaveRequestRepository;
		_emailSender = emailSender;
		_appLogger = appLogger;
	}

	public async Task Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
	{
		var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

		if (leaveRequest is null)
			throw new NotFoundException(nameof(leaveRequest), request.Id);

		leaveRequest.Approved = request.Approved;
		await _leaveRequestRepository.UpdateAsync(leaveRequest);

		// If request is approved, get and update the employee's allocations

		// Send Confirmation Email
		// Send confirmation email
		try
		{
			var email = new EmailMessage
			{
				To = string.Empty,
				Body = $"Your leave requests for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled.",
				Subject = "Leave Request Cancelled"
			};
			await _emailSender.SendEmail(email);
		}
		catch (Exception ex)
		{
			_appLogger.LogWarning(ex.Message);
		}
	}
}
