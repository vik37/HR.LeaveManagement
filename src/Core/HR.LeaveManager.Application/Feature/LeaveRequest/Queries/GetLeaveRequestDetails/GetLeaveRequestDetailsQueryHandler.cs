using AutoMapper;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
		IUserService userService)
	{
		_leaveRequestRepository = leaveRequestRepository;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
	{
		var leaveRequest = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

		if (leaveRequest is null)
			throw new NotFoundException(nameof(leaveRequest), request.Id);

		if (string.IsNullOrEmpty(_userService.UserId))
			throw new ForBiddenException("You don't have permission to access");

		leaveRequest.Employee = await _userService.GetEmployeeById(_userService.UserId!);

		return leaveRequest;
	}
}
