using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManager.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repository;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
	public LeaveAllocationRepository(HRDatabaseContext dbContext) : base(dbContext)
	{
	}

	public async Task AddAllocation(List<LeaveAllocation> allocations)
	{
		await _dbContext.AddRangeAsync(allocations);
	}

	public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
	{
		return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
																&& q.LeaveTypeId == leaveTypeId
																&& q.Period == period);
	}

	public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
	{
		var leaveAllocations = await _dbContext.LeaveAllocations
													.Include(q => q.LeaveType)
													.FirstOrDefaultAsync(q => q.Id == id);

		return leaveAllocations;
	}

	public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
	{
		var leaveAllocations = await _dbContext.LeaveAllocations
													.Include(q => q.LeaveType).ToListAsync();

		return leaveAllocations;
	}

	public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
	{
		var leaveAllocations = await _dbContext.LeaveAllocations
													.Where(q => q.EmployeeId == userId)
													.Include(q => q.LeaveType).ToListAsync();

		return leaveAllocations;
	}

	public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
	{
		return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId 
																			&& q.LeaveTypeId == leaveTypeId);
	}
}
