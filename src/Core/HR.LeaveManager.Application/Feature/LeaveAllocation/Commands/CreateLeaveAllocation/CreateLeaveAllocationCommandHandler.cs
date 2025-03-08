using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IMapper _mapper;

	public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
		ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
		_leaveTypeRepository = leaveTypeRepository;
		_mapper = mapper;
	}

	public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
	{
		var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
		var validationResult = await validator.ValidateAsync(request);

		if (validationResult.Errors.Any())
			throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

		var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

		// Get Employees

		// Get Period

		// Assigned Allocation
		var leaveAllocation = _mapper.Map<HR.LeaveManagement.Domain.LeaveAllocation>(request);

		await _leaveAllocationRepository.CreateAsync(leaveAllocation);
		return leaveAllocation.Id;
	}
}
