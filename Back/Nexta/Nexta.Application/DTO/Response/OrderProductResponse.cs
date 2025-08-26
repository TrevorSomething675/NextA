namespace Nexta.Application.DTO.Response
{
    public class OrderProductResponse
    {
		public Guid OrderId { get; init; }
		public Guid ProductId { get; init; }
        public ProductResponse Product { get; init; }

        public int Count { get; set; }
    }
}