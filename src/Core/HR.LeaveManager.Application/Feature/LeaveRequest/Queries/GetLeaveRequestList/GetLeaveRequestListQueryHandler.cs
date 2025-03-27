using AutoMapper;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
		IUserService userService)
	{
		_leaveRequestRepository = leaveRequestRepository;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
	{
		var leaveRequests = new List<LeaveManagement.Domain.LeaveRequest>();
		var requests = new List<LeaveRequestListDto>();

		if (request.IsLoggedInUser)
		{
			var userId = _userService.UserId;
			leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails(userId ?? string.Empty);

			var employee = await _userService.GetEmployeeById(userId ?? string.Empty);
			requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

			foreach (var req in requests)
			{
				req.Employee = employee;
			}
			return requests;
		}

		leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails();
		requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
		foreach (var req in requests)
		{
			req.Employee = await _userService.GetEmployeeById(req.RequestingEmployeeId);
		}

		return requests;
	}
}
