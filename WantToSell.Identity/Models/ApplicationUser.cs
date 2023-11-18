using Microsoft.AspNetCore.Identity;
using WantToSell.Domain;

namespace WantToSell.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Address? Address { get; set; }
		public virtual List<Item> Items { get; set; }
	}
}
