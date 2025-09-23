namespace Nexta.Web.Models.Products
{
    public class GetProductsRequest
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
