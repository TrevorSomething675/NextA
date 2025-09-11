using Nexta.Domain.Models.BaseModels;

namespace Nexta.Domain.Models.Images
{
    public class ProductImage : BaseImage
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
