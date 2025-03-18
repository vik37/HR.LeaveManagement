using HR.LeaveManagement.Identity.DbContext;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Models.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HR.LeaveManagement.Identity;

public static class IdentityServiceRegistration
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

		services.AddDbContext<HRLeaveManagementIdentityDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString")));

		services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<HRLeaveManagementIdentityDbContext>()
			.AddDefaultTokenProviders();

		services.AddTransient<IAuthService, AuthService>();
		services.AddTransient<IUserService, UserService>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(opt =>
		{
			opt.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
				ValidIssuer = configuration["JwtSettings:Issue"],
				ValidAudience = configuration["JwtSettings:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
			};
		});

		return services;
	}
}
