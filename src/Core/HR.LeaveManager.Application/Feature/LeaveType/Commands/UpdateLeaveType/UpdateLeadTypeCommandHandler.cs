using AutoMapper;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeadTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
	private readonly IMapper _mapper;
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public UpdateLeadTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
	{
		_mapper = mapper;
		_leaveTypeRepository = leaveTypeRepository;
	}

	public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
	{
		var updatedLeaveType = _mapper.Map<LeaveManagement.Domain.LeaveType>(request);
		await _leaveTypeRepository.UpdateAsync(updatedLeaveType);

		return Unit.Value;
	}
}
