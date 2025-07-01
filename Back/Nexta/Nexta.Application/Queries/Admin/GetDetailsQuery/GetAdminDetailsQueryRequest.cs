using MediatR;
using Nexta.Domain.Filters;

namespace Nexta.Application.Queries.Admin.GetDetailsQuery
{
    public class GetAdminDetailsQueryRequest : IRequest<GetAdminDetailsQueryResponse>
	{
		public GetDetailsFilter Filter { get; init; } = null!;
	}
}