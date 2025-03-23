using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery(bool IsLoggedInUser) : IRequest<List<LeaveRequestListDto>>;
