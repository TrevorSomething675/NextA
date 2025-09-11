namespace Nexta.Web.Models.Orders
{
    public class GetOrdersForUserRequest
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public string? SearchTerm { get; set; }
    }
}