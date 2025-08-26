namespace Nexta.Web.Models.Basket
{
    public class AddBasketProductRequest
    {
        public Guid UserId { get; set; }
        public Guid DetailId { get; set; }
        public int CountToPay { get; set; }
    }
}