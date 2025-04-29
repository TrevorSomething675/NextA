using Nexta.Domain.Enums;

namespace Nexta.Domain.Entities
{
	public class DetailEntity : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Article { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DetailStatus Status { get; set; }

		public DateTime OrderDate { get; set; }
		public DateTime DeliveryDate { get; set; }

		public int Count { get; set; }
		public int NewPrice { get; set; }
		public int OldPrice { get; set; }

		public List<UserDetailEntity>? UserDetail { get; set; }
	}
}