using WantToSell.Domain.Shared;

namespace WantToSell.Domain;

public class Subcategory : Entity
{
	public string Name { get; set; }
	public virtual Category Category { get; set; }
	public virtual Guid CategoryId { get; set; }
	
	public virtual List<Item> Items { get; set; }
}