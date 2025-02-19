using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManager.Application;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		return services;
	}
}
