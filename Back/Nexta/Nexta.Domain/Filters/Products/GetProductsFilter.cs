namespace Nexta.Domain.Filters.Products
{
    public class GetProductsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;
        public string Category { get; set; } = string.Empty;

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}