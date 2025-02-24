using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommaindHandler : IRequestHandler<DeleteLeaveTypeCommaind, Unit>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;

	public DeleteLeaveTypeCommaindHandler(ILeaveTypeRepository leaveTypeRepository)
	{
		_leaveTypeRepository = leaveTypeRepository;
	}

	public async Task<Unit> Handle(DeleteLeaveTypeCommaind request, CancellationToken cancellationToken)
	{
		var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

		if (leaveTypeToDelete is null)
			throw new NotFoundException(nameof(LeaveType), request.Id);

		await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

		return Unit.Value;
	}
}
