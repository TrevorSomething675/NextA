using Nexta.Domain.Filters.Products;
using Nexta.Application.DTO.Product;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
    public class GetAdminProductsQuery : IRequest<PagedData<AdminProductDto>>
	{
		public GetProductsFilter Filter { get; init; } = null!;
	}
}