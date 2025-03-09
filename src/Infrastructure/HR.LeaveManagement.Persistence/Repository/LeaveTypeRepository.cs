using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManager.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repository;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
	public LeaveTypeRepository(HRDatabaseContext dbContext) : base(dbContext)
	{
	}

	public async Task<bool> IsLeaveTypeUnique(string name)
	{
		return await _dbContext.LeaveTypes.AnyAsync(q => q.Name == name) == false;
	}
}
