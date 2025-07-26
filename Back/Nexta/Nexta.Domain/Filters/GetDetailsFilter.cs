namespace Nexta.Domain.Filters
{
    public class GetDetailsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;
    }
}