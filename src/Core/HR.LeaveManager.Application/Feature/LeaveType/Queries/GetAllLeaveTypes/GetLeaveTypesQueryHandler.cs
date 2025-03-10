using AutoMapper;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

	public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<GetLeaveTypesQueryHandler> logger)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_logger = logger;
	}

	public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
	{
		var entity = await _leaveTypeRepository.GetAsynt();

		_logger.LogInformation("Leave Type was retreived successfully");
		return _mapper.Map<List<LeaveTypeDto>>(entity);
	}
}
