using HR.LeaveManagement.Domain;

namespace HR.LeaveManager.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
}