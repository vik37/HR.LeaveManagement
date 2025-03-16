using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		var hasher = new PasswordHasher<ApplicationUser>();
		builder.HasData(
				new ApplicationUser
				{
					Id = "16f66949-e034-4664-ad8a-2208dfdfdb0e",
					Email = "admin@localhost.com",
					NormalizedEmail = "ADMIN@LOCALHOST.COM",
					Firstname = "System",
					Lastname = "Admin",
					UserName = "admin@localhost.com",
					NormalizedUserName = "ADMIN@LOCALHOST.COM",
					PasswordHash = hasher.HashPassword(null, "P@ssword1#9T"),
					EmailConfirmed = true
				},
				new ApplicationUser
				{
					Id = "3288b8b1-f1f8-4256-b88c-5791ce6e7f7a",
					Email = "user@localhost.com",
					NormalizedEmail = "USER@LOCALHOST.COM",
					Firstname = "System",
					Lastname = "User",
					UserName = "user@localhost.com",
					NormalizedUserName = "USER@LOCALHOST.COM",
					PasswordHash = hasher.HashPassword(null, "Password#9"),
					EmailConfirmed = true
				}
			);
	}
}
