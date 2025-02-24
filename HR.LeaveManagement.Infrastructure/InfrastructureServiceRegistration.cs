using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, 
		IConfiguration configuration)
	{

		return services;
	}
}
