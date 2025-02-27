using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManager.Application.Contracts.Logging;

namespace HR.LeaveManager.Application.Feature.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommaindHandler : IRequestHandler<DeleteLeaveTypeCommaind, Unit>
{
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IAppLogger<DeleteLeaveTypeCommaindHandler> _logger;

	public DeleteLeaveTypeCommaindHandler(ILeaveTypeRepository leaveTypeRepository,
		IAppLogger<DeleteLeaveTypeCommaindHandler> logger)
	{
		_leaveTypeRepository = leaveTypeRepository;
		_logger = logger;
	}

	public async Task<Unit> Handle(DeleteLeaveTypeCommaind request, CancellationToken cancellationToken)
	{
		var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

		if (leaveTypeToDelete is null)
		{
			_logger.LogWarning("Invalid {0} - {1}", nameof(LeaveType)
				, request.Id);
			throw new NotFoundException(nameof(LeaveType), request.Id);
		}

		await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

		_logger.LogInformation("{0} with id {1} was removed successfully.", nameof(LeaveType), request.Id);
		return Unit.Value;
	}
}
