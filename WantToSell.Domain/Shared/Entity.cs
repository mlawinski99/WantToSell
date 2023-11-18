namespace WantToSell.Domain.Shared
{
	public abstract class Entity
	{
		public Guid Id { get; set; }
		public DateTime DateCreatedUtc { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime? DateModifiedUtc { get; set;}
	}
}
