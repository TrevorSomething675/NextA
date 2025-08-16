using Nexta.Domain.Models.BaseModels;

namespace Nexta.Domain.Models.Images
{
    public class DetailImage : BaseImage
    {
        public Guid DetailId { get; set; }
        public Detail Detail { get; set; }
    }
}
