using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HRDatabaseContext : DbContext
{
	private readonly IUserService _userService;
	public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options, 
		IUserService userService) 
		: base(options)
	{
		_userService = userService;
	}

	public DbSet<LeaveType> LeaveTypes { get; set; }
	public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
	public DbSet<LeaveRequest> LeaveRequests { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDatabaseContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
			.Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
		{
			entry.Entity.DateModified = DateTime.Now;
			entry.Entity.ModifiedBy = _userService.UserId;

			entry.Property(e => e.DateCreated).IsModified = false;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.DateCreated = DateTime.Now;
				entry.Entity.CreatedBy = _userService.UserId;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
