using AutoMapper;
using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models.Emails;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IMapper _mapper;
	private readonly IEmailSender _emailSender;
	private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

	public UpdateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
		ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender,
		IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveRequestRepository = leaveRequestRepository;
		_mapper = mapper;
		_emailSender = emailSender;
		_appLogger = appLogger;
	}

	public async Task Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
	{
		var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

		if (leaveRequest is null)
			throw new NotFoundException(nameof(leaveRequest), request.Id);

		var validator = new UpdateLeaveRequestValidator(_leaveTypeRepository, _leaveRequestRepository);
		var validationResults = await validator.ValidateAsync(request);

		if (validationResults.Errors.Any())
			throw new BadRequestException("Invalid Leave Request", validationResults);

		_mapper.Map(request, leaveRequest);

		await _leaveRequestRepository.UpdateAsync(leaveRequest);

		try
		{
			var email = new EmailMessage
			{
				To = string.Empty,
				Body = $"Your leave requests for {request.StartDate:D} to {request.EndDate:D} has been updated successfully.",
				Subject = "Leave Request Submitted"
			};
			await _emailSender.SendEmail(email);
		}
		catch (Exception ex)
		{
			_appLogger.LogWarning(ex.Message);
		}
	}
}
