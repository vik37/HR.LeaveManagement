using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IMapper _mapper;

	public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
	{
		_leaveRequestRepository = leaveRequestRepository;
		_mapper = mapper;
	}

	public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
	{
		var requests = await _leaveRequestRepository.GetAsynt();
		return _mapper.Map<List<LeaveRequestListDto>>(request);
	}
}
