using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;
using WantToSell.Domain;
using WantToSell.Domain.Shared;

namespace WantToSell.Persistence.DbContext
{
	public class WantToSellContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public WantToSellContext(DbContextOptions<WantToSellContext> options) : base(options)
		{
				
		}
		
		public DbSet<Category> Categories { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in base.ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.DateCreatedUtc = DateTime.UtcNow;
					entry.Entity.DateModifiedUtc = null;
					//@todo createdby
					//entry.Entity.CreatedBy = User
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Entity.DateModifiedUtc = DateTime.UtcNow;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
