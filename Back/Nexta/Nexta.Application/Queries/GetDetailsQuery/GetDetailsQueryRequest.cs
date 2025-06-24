using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetDetailsQuery
{
    public class GetDetailsQueryRequest : IRequest<GetDetailsQueryResponse>
    {
		public GetDetailsFilter Filter { get; set; } = null!;
	}
}