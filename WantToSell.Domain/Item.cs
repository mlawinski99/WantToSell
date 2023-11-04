using WantToSell.Domain.Shared;

namespace WantToSell.Domain;

public class Item : Entity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime DateExpiredUtc { get; set; }
	public string Condition { get; set; }
	public virtual Category Category { get; set; }
	public virtual Subcategory Subcategory { get; set; }
}