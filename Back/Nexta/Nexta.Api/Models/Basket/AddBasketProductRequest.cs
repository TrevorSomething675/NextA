namespace Nexta.Web.Models.Basket
{
    public class AddBasketProductRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int CountToPay { get; set; }
    }
}