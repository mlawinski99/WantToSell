namespace WantToSell.Application.Features.Address.Models
{
	public class AddressUpdateModel
	{
		public Guid Id { get; set; }
		public string ApartmentNumber { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string PostalCode { get; set; }
	}
}
