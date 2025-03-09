using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDelailsQueryHandler : IRequestHandler<LeaveAllocationDelailsQuery, LeaveAllocationDetailsDto>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly IMapper _mapper;

	public LeaveAllocationDelailsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
		_mapper = mapper;
	}

	public async Task<LeaveAllocationDetailsDto> Handle(LeaveAllocationDelailsQuery request, CancellationToken cancellationToken)
	{
		var leaveAllocationDetails = await _leaveAllocationRepository.GetByIdAsync(request.Id);
		return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);
	}
}
