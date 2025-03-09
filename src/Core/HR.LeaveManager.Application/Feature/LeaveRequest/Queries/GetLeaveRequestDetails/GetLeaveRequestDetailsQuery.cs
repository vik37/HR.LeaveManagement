using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>;
