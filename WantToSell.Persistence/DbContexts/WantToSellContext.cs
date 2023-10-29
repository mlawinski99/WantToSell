using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Domain;
using WantToSell.Domain.Shared;

namespace WantToSell.Persistence.DbContext
{
	public class WantToSellContext : Microsoft.EntityFrameworkCore.DbContext
	{
		private readonly IUserService _userService;
		public WantToSellContext(DbContextOptions<WantToSellContext> options
			, IUserService userService) : base(options)
		{
			this._userService = userService;
		}
		
		public DbSet<Category> Categories { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in base.ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.DateCreatedUtc = DateTime.UtcNow;
						entry.Entity.DateModifiedUtc = null;
						entry.Entity.CreatedBy = _userService.GetCurrentUserId();
						break;
					case EntityState.Modified:
						entry.Entity.DateModifiedUtc = DateTime.UtcNow;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
