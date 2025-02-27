using AutoMapper;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<GetLeaveTypesHandler> _logger;

	public GetLeaveTypesHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<GetLeaveTypesHandler> logger)
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
