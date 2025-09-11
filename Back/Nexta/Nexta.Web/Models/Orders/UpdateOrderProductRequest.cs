namespace Nexta.Web.Models.Orders
{
    public class UpdateOrderProductRequest
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}
