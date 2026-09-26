namespace Nexta.Web.Models.Basket
{
    public class DeleteBasketProductRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
