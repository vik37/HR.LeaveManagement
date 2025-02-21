using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
	}

	public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
	{
		// Validate Incoming Data

		// Convert to domain object
		var leaveTypeCreated = _mapper.Map<LeaveManagement.Domain.LeaveType>(request);

		// Add to database
		await _leaveTypeRepository.CreateAsync(leaveTypeCreated);

		// return record id
		return leaveTypeCreated.Id;
	}
}
