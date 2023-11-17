using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WantToSell.Identity.Configurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(
				new IdentityUserRole<string> //admin
				{
					RoleId = "4a015cc4-da5d-4b0a-8296-4a26e85a81de",
					UserId = "ad098c34-065f-4c57-85af-ce2bcf28e7da"
				},
				new IdentityUserRole<string> //user
				{
					RoleId = "dce89f2e-8c4c-47e5-bd03-33bcaa7edccc",
					UserId = "342888de-992a-47ff-b3fd-285735f5c6b2"
				});
		}
	}
}
