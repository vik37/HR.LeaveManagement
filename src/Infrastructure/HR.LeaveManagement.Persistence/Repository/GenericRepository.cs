using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManager.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	protected readonly HRDatabaseContext _dbContext;

	public GenericRepository(HRDatabaseContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IReadOnlyList<T>> GetAsynt()
	{
		return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
	}

	public async Task<T> GetByIdAsync(int id)
	{
		return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
	}

	public async Task CreateAsync(T entity)
	{
		await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(T entity)
	{
		_dbContext.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(T entity)
	{
		_dbContext.Entry(entity).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
	}
}
