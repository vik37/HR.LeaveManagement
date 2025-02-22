using AutoMapper;
using HR.LeaveManager.Application.Contracts.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
	}

	public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
	{
		var leaveType = await _leaveTypeRepository.GetByIdAsync(request.id);

		if (leaveType is null)
			throw new NotFoundException(nameof(LeaveType), request.id);

		return _mapper.Map<LeaveTypeDetailsDto>(leaveType);
	}
}
