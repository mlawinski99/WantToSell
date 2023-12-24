using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WantToSell.Identity.Models;

namespace WantToSell.Identity.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();
			builder.HasData(
				new ApplicationUser
				{
					Id= "ad098c34-065f-4c57-85af-ce2bcf28e7da",
					Email = "admin@admin.com",
					NormalizedEmail = "ADMIN@ADMIN.COM",
					FirstName = "System",
					LastName = "Administrator",
					UserName = "admin@admin.com",
					NormalizedUserName = "ADMIN@ADMIN.COM",
					PasswordHash = hasher.HashPassword(null, "<47?7Ctp"),
					EmailConfirmed = true,
				},
				new ApplicationUser
				{
					Id = "342888de-992a-47ff-b3fd-285735f5c6b2",
					Email = "user@user.com",
					NormalizedEmail = "USER@USER.COM",
					FirstName = "Test",
					LastName = "User",
					UserName = "user@user.com",
					NormalizedUserName = "USER@USER.COM",
					PasswordHash = hasher.HashPassword(null, "<47?7Ctp"),
					EmailConfirmed = true,
				}
				);
		}
	}
}
