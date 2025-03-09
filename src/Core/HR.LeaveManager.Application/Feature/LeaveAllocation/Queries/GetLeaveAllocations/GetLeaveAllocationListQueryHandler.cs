using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly IMapper _mapper;

	public GetLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
		_mapper = mapper;
	}

	public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
	{
		var leaveAllocations = await _leaveAllocationRepository.GetAsynt();
		var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

		return allocations;
	}
}
