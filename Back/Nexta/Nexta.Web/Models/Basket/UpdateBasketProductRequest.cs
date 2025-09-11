namespace Nexta.Web.Models.Basket
{
    public class UpdateBasketProductRequest
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public DateOnly? DeliveryDate { get; init; }
        public int? Count { get; init; }
    }
}
