using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public record LeaveAllocationDelailsQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;
