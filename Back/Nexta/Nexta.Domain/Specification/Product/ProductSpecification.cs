using Nexta.Domain.Models.Product;

namespace Nexta.Domain.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(int pageNumber, int pageSize)
        {
            AddInclude(p => p.Images);
        }
    }
}