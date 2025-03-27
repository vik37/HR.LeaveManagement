using AutoMapper;
using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Identity;
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
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly IMapper _mapper;
	private readonly IEmailSender _emailSender;
	private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;
	private readonly IUserService _userService;

	public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
		ILeaveRequestRepository leaveRequestRepository, ILeaveAllocationRepository leaveAllocationRepository,
		IMapper mapper, IEmailSender emailSender, 
		IAppLogger<CreateLeaveRequestCommandHandler> appLogger, IUserService userService)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_leaveRequestRepository = leaveRequestRepository;
		_leaveAllocationRepository = leaveAllocationRepository;
		_mapper = mapper;
		_emailSender = emailSender;
		_appLogger = appLogger;
		_userService = userService;
	}

	public async Task Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
	{
		var validator = new CreateLeaveRequestValidator(_leaveTypeRepository);
		var validationResults = await validator.ValidateAsync(request);

		if (validationResults.Errors.Any())
			throw new BadRequestException("Invalid Leave Request", validationResults);

		// Get requesting employee id's
		var employeeId = _userService.UserId;

		// Check on Employee Allocations
		var allocation = await _leaveAllocationRepository.
			GetUserAllocations(employeeId??throw new BadRequestException("Invalid Request"), request.LeaveTypeId);

		// If allocations aren't enough, return validation error results
		if(allocation is null)
		{
			validationResults.Errors.Add(new FluentValidation.Results.ValidationFailure(
				nameof(request.LeaveTypeId), "You do not have any allocation for this leave type."));
			throw new BadRequestException("Invalid Leave Request",validationResults);
		}

		int requestedDays = (int)(request.EndDate - request.StartDate).TotalDays;
		if (requestedDays > allocation.NumberOfDays)
		{
			validationResults.Errors.Add(new FluentValidation.Results.ValidationFailure(
				nameof(request.EndDate), "You do not have enaugh days for this request."));
			throw new BadRequestException("Invalid Leave Request", validationResults);
		}

		// Create Leave Request
		var leaveRequest = _mapper.Map<HR.LeaveManagement.Domain.LeaveRequest>(request);
		leaveRequest.RequestingEmployeeId = employeeId;
		leaveRequest.DateRequested = DateTime.Now;
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
