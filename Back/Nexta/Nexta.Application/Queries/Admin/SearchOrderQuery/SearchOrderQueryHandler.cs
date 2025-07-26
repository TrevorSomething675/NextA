using MediatR;

namespace Nexta.Application.Queries.Admin.SearchOrderQuery
{
    public class SearchOrderQueryHandler : IRequestHandler<SearchOrderQueryRequest, SearchOrderQueryResponse>
    {
        public async Task<SearchOrderQueryResponse> Handle(SearchOrderQueryRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}