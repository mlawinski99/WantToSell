using WantToSell.Domain.Shared;

namespace WantToSell.Domain;

public class Address : Entity
{
	public string ApartmentNumber { get; set; }
	public string City { get; set; }
	public string Street { get; set; }
	public string PostalCode { get; set; }
}