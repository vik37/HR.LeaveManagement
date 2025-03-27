using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManager.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repository;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
	public LeaveRequestRepository(HRDatabaseContext dbContext) : base(dbContext)
	{
	}

	public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
	{
		var leaveRequest = await _dbContext.LeaveRequests
											.Where(q => !string.IsNullOrEmpty(q.RequestingEmployeeId))
											.Include(q => q.LeaveType)
											.ToListAsync();
		return leaveRequest;
	}

	public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
	{
		var leaveRequest = await _dbContext.LeaveRequests								
											.Include(q => q.LeaveType)
											.FirstOrDefaultAsync(q => q.Id == id);
		return leaveRequest;
	}

	public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
	{
		var leaveRequest = await _dbContext.LeaveRequests
											.Where(q => q.RequestingEmployeeId == userId)
											.Include(q => q.LeaveType)
											.ToListAsync();
		return leaveRequest;
	}
}
