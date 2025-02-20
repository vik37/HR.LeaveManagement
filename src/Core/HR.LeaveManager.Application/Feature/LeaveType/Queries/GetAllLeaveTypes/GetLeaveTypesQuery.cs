using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
{
}
