using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetDetailsQuery
{
    public class GetAdminDetailsQueryRequest : IRequest<GetAdminDetailsQueryResponse>
	{
		public GetDetailsFilter Filter { get; init; } = null!;
	}
}