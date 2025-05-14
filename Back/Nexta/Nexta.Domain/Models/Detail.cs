using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class Detail
    {
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Article { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DetailStatus Status { get; set; }

		public string OrderDate { get; set; } = string.Empty;
		public string DeliveryDate { get; set; } = string.Empty;

		public int Count { get; set; } 
		public int NewPrice { get; set; }
		public int OldPrice { get; set; }

		public List<UserDetail>? UserDetail { get; set; }
	}
}