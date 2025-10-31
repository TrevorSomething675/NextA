using Nexta.Application.DTO.Products;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
    public class GetAdminProductsQuery : IRequest<PagedData<AdminProductDto>>
	{
        public string SearchTerm { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}