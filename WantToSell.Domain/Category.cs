using System.Dynamic;
using System.Net.Sockets;
using System.Reflection.Metadata;
using WantToSell.Domain.Shared;

namespace WantToSell.Domain
{
	public class Category : Entity
	{
		public string Name { get; set; }
		public virtual List<Subcategory> Subcategories { get; set; }
	}
}