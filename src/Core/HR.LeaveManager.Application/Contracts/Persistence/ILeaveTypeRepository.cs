using HR.LeaveManagement.Domain;

namespace HR.LeaveManager.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
	Task<bool> IsLeaveTypeUnique(string name);
}
