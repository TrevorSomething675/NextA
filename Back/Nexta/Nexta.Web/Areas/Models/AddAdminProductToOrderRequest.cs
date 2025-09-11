namespace Nexta.Web.Areas.Models
{
    public class AddAdminProductToOrderRequest
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}