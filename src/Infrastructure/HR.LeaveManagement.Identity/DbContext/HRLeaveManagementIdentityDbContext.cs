using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DbContext;

public class HRLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
	public HRLeaveManagementIdentityDbContext(DbContextOptions<HRLeaveManagementIdentityDbContext> options)
		: base(options)
	{
		
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{		
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(HRLeaveManagementIdentityDbContext).Assembly);
	}
}
