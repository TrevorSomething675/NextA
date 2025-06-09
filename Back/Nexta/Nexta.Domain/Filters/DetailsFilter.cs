namespace Nexta.Domain.Filters
{
    public class DetailsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageSize { get; set; } = 16;
    }
}