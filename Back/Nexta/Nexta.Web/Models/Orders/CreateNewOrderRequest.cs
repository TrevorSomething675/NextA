namespace Nexta.Web.Models.Orders
{
    public class CreateNewOrderRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}