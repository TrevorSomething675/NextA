namespace Nexta.Web.Models.Products
{
    public class GetProductsRequest
    {
        public string SearchTerm { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
