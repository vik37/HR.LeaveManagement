using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

public record GetLeaveTypeDetailsQuery(int id) : IRequest<LeaveTypeDetailsDto>;
