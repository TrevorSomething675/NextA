namespace Nexta.Application.DTO.Request
{
    public class OrderProductsRequest
    {
		public int Count { get; set; }
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
	}
}