using AutoMapper;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public GetLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepository,
		IUserService userService, IMapper mapper)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
	{
		if (request.IsLoggedInUser)
		{
			var leaveAllocationUserDetails = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(_userService.UserId??string.Empty);
			return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocationUserDetails);
		}

		var leaveAllocations = await _leaveAllocationRepository.GetAsynt();
		return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
	}
}
