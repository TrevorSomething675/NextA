namespace Nexta.Domain.Entities
{
	public class UserDetailEntity
	{
		public Guid UserId { get; set; }
		public UserEntity User { get; set; } = null!;

		public Guid DetailId { get; set; }
		public DetailEntity Detail { get; set; } = null!;
	}
}