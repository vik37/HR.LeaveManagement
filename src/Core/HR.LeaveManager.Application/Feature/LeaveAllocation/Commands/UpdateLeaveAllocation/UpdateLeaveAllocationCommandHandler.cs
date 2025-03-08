using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;

	public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, 
		ILeaveAllocationRepository leaveAllocationRepository)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_leaveAllocationRepository = leaveAllocationRepository;
	}

	public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
	{
		var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);

		var validationResult = await validator.ValidateAsync(request);

		if (validationResult.Errors.Any())
			throw new BadRequestException("Invalid Leave Allocation", validationResult);

		var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
		if (leaveAllocation is null)
			throw new NotFoundException(nameof(LeaveAllocation), request.Id);

		_mapper.Map(request, leaveAllocation);

		await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

		return Unit.Value;
	}
}
