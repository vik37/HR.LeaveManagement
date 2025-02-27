﻿using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Infrastructure.Logging;
using HR.LeaveManager.Application.Contracts.Email;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, 
		IConfiguration configuration)
	{
		services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings").Bind(c));
		services.AddTransient<IEmailSender, EmailSender>();
		services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

		return services;
	}
}
