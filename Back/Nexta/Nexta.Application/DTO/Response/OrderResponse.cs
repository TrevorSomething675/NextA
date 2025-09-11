using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Response
{
    public class OrderResponse
    {
		public Guid Id { get; init; }
		public Guid UserId { get; init; }

		public UserResponse User { get; init; } = null!;

		public List<ProductResponse>? Products { get; init; }
        public List<OrderProductResponse>? OrderProducts { get; set; }

        public string CreatedDate { get; init; } = string.Empty;
		public OrderStatus Status { get; init; }
	}
}