using AutoMapper;
using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models.Emails;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IMapper _mapper;
	private readonly IEmailSender _emailSender;
	private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

	public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
		ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender, 
		IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveRequestRepository = leaveRequestRepository;
		_mapper = mapper;
		_emailSender = emailSender;
		_appLogger = appLogger;
	}

	public async Task Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
	{
		var validator = new CreateLeaveRequestValidator(_leaveTypeRepository);
		var validationResults = await validator.ValidateAsync(request);

		if (validationResults.Errors.Any())
			throw new BadRequestException("Invalid Leave Request", validationResults);

		// Get requesting employee id's

		// Check on Employee Allocations

		// If allocations aren't enough, return validation error results

		// Create Leave Request
		var leaveRequest = _mapper.Map<HR.LeaveManagement.Domain.LeaveRequest>(request);
		await _leaveRequestRepository.CreateAsync(leaveRequest);

		try
		{
			var email = new EmailMessage
			{
				To = string.Empty,
				Body = $"Your leave requests for {request.StartDate:D} to {leaveRequest.EndDate:D} has been created successfully.",
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
