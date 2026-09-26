namespace Nexta.Web.Areas.Models
{
    public class DeleteProductFromOrderRequest
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
    }
}
