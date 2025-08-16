namespace Nexta.Application.DTO.Request
{
    public class OrderDetailsRequest
    {
		public int Count { get; set; }
		public Guid OrderId { get; set; }
		public Guid DetailId { get; set; }
	}
}