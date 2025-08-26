namespace Nexta.Web.Models.Orders
{
    public class UpdateOrderProductRequest
    {
        public Guid OrderId { get; init; }
        public Guid DetailId { get; init; }
        public int Count { get; init; }
    }
}
