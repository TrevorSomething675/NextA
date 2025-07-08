namespace Nexta.Application.DTO.RequestModels
{
    public class OrderDetailsRequest
    {
		public int Count { get; set; }
		public Guid OrderId { get; set; }
		public Guid DetailId { get; set; }
	}
}