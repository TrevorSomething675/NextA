using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class UserDetail
    {
        public Guid UserId { get; set; }
        public Guid DetailId { get; set; }
		public int Count { get; set; }
        public string DeliveryDate { get; set; } = string.Empty;
        public UserDetailStatus Status { get; set; }
	}
}