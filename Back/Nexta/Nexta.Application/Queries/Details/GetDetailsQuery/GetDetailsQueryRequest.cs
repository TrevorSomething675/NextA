using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Details.GetDetailsQuery
{
    public class GetDetailsQueryRequest : IRequest<GetDetailsQueryResponse>
    {
		public GetDetailsFilter Filter { get; init; } = null!;
	}
}