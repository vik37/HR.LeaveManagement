using HR.LeaveManagement.Domain;

namespace HR.LeaveManager.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<List<T>> GetAsynt();
	Task<T> GetByIdAsync(int id);
	Task<T> CreateAsync(T entity);
	Task<T> UpdateASync(T entity);
	Task<T> DeleteAsync(T entity);	
}
