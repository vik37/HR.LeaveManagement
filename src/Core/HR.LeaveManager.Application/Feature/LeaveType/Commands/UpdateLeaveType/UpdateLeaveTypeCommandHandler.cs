using AutoMapper;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

	public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<UpdateLeaveTypeCommandHandler> logger)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_logger = logger;
	}

	public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
	{
		var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
		var validatorResult = await validator.ValidateAsync(request);

		if (!validatorResult.IsValid)
		{
			_logger.LogWarning("Validation errors in update requests for {0} - {1}", nameof(LeaveType)
				,request.Id);
			throw new BadRequestException("Invalid Leave Type", validatorResult);
		}
			
		var updatedLeaveType = _mapper.Map<LeaveManagement.Domain.LeaveType>(request);

		await _leaveTypeRepository.UpdateAsync(updatedLeaveType);

		_logger.LogInformation("{0} - {1} was successfully updated.", nameof(LeaveType), request.Id);
		return Unit.Value;
	}
}
