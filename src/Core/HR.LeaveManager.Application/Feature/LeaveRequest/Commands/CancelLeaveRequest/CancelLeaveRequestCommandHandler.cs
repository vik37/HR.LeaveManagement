using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand>
{
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IEmailSender _emailSender;
	private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

	public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender,
		IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
	{
		_leaveRequestRepository = leaveRequestRepository;
		_emailSender = emailSender;
		_appLogger = appLogger;
	}

	public async Task Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
	{
		var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

		if (leaveRequest is null)
			throw new NotFoundException(nameof(leaveRequest), request.Id);

		leaveRequest.Cancelled = true;

		// Re-evaluate the employees allocations for the leave type

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
