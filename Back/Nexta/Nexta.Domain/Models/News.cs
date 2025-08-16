using Nexta.Domain.Models.Images;

namespace Nexta.Domain.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }

        public NewsImage? Image { get; set; }
    }
}