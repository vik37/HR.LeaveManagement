using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;

	public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
	}

	public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
	{
		var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

		if (leaveAllocation is null)
			throw new NotFoundException(nameof(LeaveAllocation), request.Id);

		await _leaveAllocationRepository.DeleteAsync(leaveAllocation);
	}
}
