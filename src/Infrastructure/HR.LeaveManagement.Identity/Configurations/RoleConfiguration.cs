using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		builder.HasData(
				new IdentityRole
				{
					Id = "01597a37-2a70-4ac8-ad64-2d01fbdaa48c",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				},
				new IdentityRole
				{
					Id = "36186e03-0c07-49e9-b2ca-f851aef83c60",
					Name = "Employee",
					NormalizedName = "EMPLOYEE"
				}				
			);
	}
}
