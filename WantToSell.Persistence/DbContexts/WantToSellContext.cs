using Microsoft.EntityFrameworkCore;
using WantToSell.Domain;
using WantToSell.Domain.Interfaces;
using WantToSell.Domain.Shared;

namespace WantToSell.Persistence.DbContexts
{
	public class WantToSellContext : DbContext
	{
		private IUserContext _userContext;
		public WantToSellContext(DbContextOptions<WantToSellContext> options, IUserContext userContext) : base(options)
		{
			_userContext = userContext;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Item>()
				.HasOne(r => r.Subcategory)
				.WithMany(p => p.Items)
				.HasForeignKey(r => r.SubcategoryId)
				.OnDelete(DeleteBehavior.NoAction);
			
			modelBuilder.Entity<Item>()
				.HasOne(r => r.Category)
				.WithMany(p => p.Items)
				.HasForeignKey(r => r.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);
		}
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Subcategory> Subcategories { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in base.ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.DateCreatedUtc = DateTime.UtcNow;
						entry.Entity.DateModifiedUtc = null;
						entry.Entity.CreatedBy = _userContext.UserId;
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
