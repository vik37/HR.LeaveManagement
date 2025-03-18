using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		builder.HasData(
				new IdentityUserRole<string>
				{
					RoleId = "01597a37-2a70-4ac8-ad64-2d01fbdaa48c",
					UserId = "16f66949-e034-4664-ad8a-2208dfdfdb0e"
				},
				new IdentityUserRole<string>
				{
					RoleId = "36186e03-0c07-49e9-b2ca-f851aef83c60",
					UserId = "3288b8b1-f1f8-4256-b88c-5791ce6e7f7a"
				}
			);
	}
}
