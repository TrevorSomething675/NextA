namespace Nexta.Web.Models.Orders
{
    public class CreateNewOrderRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> DetailIds { get; set; }
    }
}