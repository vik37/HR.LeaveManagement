using AutoMapper;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Identity;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IUserService _userService;
	private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> _logger;

	public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IUserService userService,
		IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_userService = userService;
		_logger = logger;
	}

	public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(request.id);

		if (leaveType is null)
			throw new NotFoundException(nameof(LeaveType), request.id);

		_logger.LogInformation("Leave Type was retreived successfully");
	    var leaveTypeDto = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

		if (_userService.Role == "Administrator")
		{
			if (!string.IsNullOrEmpty(leaveType.CreatedBy))
			{
				var user = await _userService.GetEmployeeById(leaveType.CreatedBy);
				leaveTypeDto.CreatedBy = user.Email;
			}

			if (!string.IsNullOrEmpty(leaveType.ModifiedBy))
			{
				var user = await _userService.GetEmployeeById(leaveType.ModifiedBy);
				leaveTypeDto.ModifiedBy = user.Email;
			}
		}

		return leaveTypeDto;
	}
}
