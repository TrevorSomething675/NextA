using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetDetailsQuery
{
    public class GetDetailsQueryRequest(DetailsFilter filter) : IRequest<Result<GetDetailsQueryResponse>>
    {
		public DetailsFilter Filter { get; set; } = filter;
	}
}