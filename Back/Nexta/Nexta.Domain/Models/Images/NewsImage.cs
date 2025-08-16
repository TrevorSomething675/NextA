using Nexta.Domain.Models.BaseModels;

namespace Nexta.Domain.Models.Images
{
    public class NewsImage : BaseImage
    {
        public News News { get; set; }
        public string Base64String { get; set; }
    }
}
