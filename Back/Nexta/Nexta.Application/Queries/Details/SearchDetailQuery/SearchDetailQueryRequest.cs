using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Details.SearchDetailQuery
{
    public class SearchDetailQueryRequest : IRequest<SearchDetailQueryResponse>
    {
        public SearchDetailFilter Filter { get; set; } = null!;
    }
}