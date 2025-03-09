using AutoMapper;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManager.Application.Contracts.Logging;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> _logger;

	public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
		_logger = logger;
	}

	public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(request.id);

		if (leaveType is null)
			throw new NotFoundException(nameof(LeaveType), request.id);

		_logger.LogInformation("Leave Type was retreived successfully");
		return _mapper.Map<LeaveTypeDetailsDto>(leaveType);
	}
}
