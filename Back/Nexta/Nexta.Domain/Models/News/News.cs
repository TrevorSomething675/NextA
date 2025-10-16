using Nexta.Domain.Base;

namespace Nexta.Domain.Models.News
{
    public class News : Entity
    {
        public string? Header { get; }
        public string? Description { get; }
        public string? Base64String { get; }
    }
}