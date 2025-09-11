namespace Nexta.Web.Areas.Models
{
    public class GetAdminProductsRequest
    {
        public string? SearchTerm { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}