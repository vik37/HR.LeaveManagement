using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
