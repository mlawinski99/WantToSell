namespace WantToSell.Application.Models.Identity
{
	public class TokenSettings
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public long DurationMinutes { get; set; }
	}
}
