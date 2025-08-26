using Nexta.Domain.Filters.Products;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
    public class GetAdminProductsQuery : IRequest<GetAdminProductsQueryResponse>
	{
		public GetProductsFilter Filter { get; init; } = null!;
	}
}