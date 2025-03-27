using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;

public record GetLeaveAllocationListQuery(bool IsLoggedInUser) : IRequest<List<LeaveAllocationDto>>;
