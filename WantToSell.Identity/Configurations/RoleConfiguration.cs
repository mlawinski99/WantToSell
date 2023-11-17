using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WantToSell.Identity.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole
				{
					Id = "dce89f2e-8c4c-47e5-bd03-33bcaa7edccc",
					Name = "User",
					NormalizedName = "USER"
				},
				new IdentityRole
				{
					Id = "4a015cc4-da5d-4b0a-8296-4a26e85a81de",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				});
		}
	}
}
