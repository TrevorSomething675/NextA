using Nexta.Application.DTO.Products;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQuery : IRequest<PagedData<ProductDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool WithHidden { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}