using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>;
