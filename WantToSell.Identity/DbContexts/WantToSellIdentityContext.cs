using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WantToSell.Domain;
using WantToSell.Identity.Models;

namespace WantToSell.Identity.DbContexts
{
	public class WantToSellIdentityContext : IdentityDbContext<ApplicationUser>
	{
		public WantToSellIdentityContext
			(DbContextOptions<WantToSellIdentityContext> options) : base(options)
		{
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(WantToSellIdentityContext).Assembly);
		}
	}
}
