using WantToSell.Domain.Shared;

namespace WantToSell.Domain
{
	public class Category : Entity
	{
		public string Name { get; set; }
		public virtual List<Subcategory> Subcategories { get; set; }
		public virtual List<Item> Items { get; set; }
	}
}