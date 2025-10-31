using Nexta.Application.Abstractions.Specification;
using Nexta.Domain.Models.Products;

namespace Nexta.Domain.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(int pageNumber, int pageSize)
        {
            AddInclude("Images");
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}