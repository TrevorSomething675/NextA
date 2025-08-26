namespace Nexta.Domain.Filters.Products
{
    public class GetProductsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;
    }
}
