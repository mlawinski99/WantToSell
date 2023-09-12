namespace WantToSell.Domain;

public class ApplicationUser
{
	public Address Address { get; set; }
	public virtual List<Item> Items { get; set; }
}