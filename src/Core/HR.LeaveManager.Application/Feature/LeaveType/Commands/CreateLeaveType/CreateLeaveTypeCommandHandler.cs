using AutoMapper;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManager.Application.Contracts.Logging;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

	public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<CreateLeaveTypeCommandHandler> logger)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_logger = logger;
	}

	public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
	{
		// Validate Incoming Data
		var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
		var validatorResult = await validator.ValidateAsync(request);

		if (!validatorResult.IsValid)
		{
			_logger.LogWarning("Validation errors in requests for creation new {0}", nameof(LeaveType));

			throw new BadRequestException("Invalid Leave Type", validatorResult);
		}
			
		// Convert to domain object
		var leaveTypeCreated = _mapper.Map<LeaveManagement.Domain.LeaveType>(request);

		// Add to database
		await _leaveTypeRepository.CreateAsync(leaveTypeCreated);

		_logger.LogInformation("{0} was successfully created.", nameof(LeaveType));
		// return record id
		return leaveTypeCreated.Id;
	}
}
