using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetDetailsQuery
{
    public class GetDetailsQueryRequest : IRequest<GetDetailsQueryResponse>
    {
		public DetailsFilter Filter { get; set; } = null!;
	}
}