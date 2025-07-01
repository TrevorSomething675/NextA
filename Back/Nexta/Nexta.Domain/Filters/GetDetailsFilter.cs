namespace Nexta.Domain.Filters
{
    public class GetDetailsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageSize { get; set; } = 16;
        public bool WithHidden { get; set; } = false;
    }
}