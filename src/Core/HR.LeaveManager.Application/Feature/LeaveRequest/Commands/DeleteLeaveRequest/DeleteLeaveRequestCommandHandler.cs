using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
{
	private readonly ILeaveRequestRepository _leaveRequestRepository;

	public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
	{
		_leaveRequestRepository = leaveRequestRepository;
	}

	public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
	{
		var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

		if (leaveRequest is null)
			throw new NotFoundException(nameof(LeaveRequest), request.Id);

		await _leaveRequestRepository.DeleteAsync(leaveRequest);
	}
}
