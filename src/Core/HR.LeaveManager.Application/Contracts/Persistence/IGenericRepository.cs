using HR.LeaveManagement.Domain;

namespace HR.LeaveManager.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<IReadOnlyList<T>> GetAsynt();
	Task<T> GetByIdAsync(int id);
	Task CreateAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);	
}
