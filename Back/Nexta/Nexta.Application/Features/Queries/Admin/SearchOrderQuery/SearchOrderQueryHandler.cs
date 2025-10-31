using MediatR;
using Nexta.Application.DTO.Orders;

namespace Nexta.Application.Queries.Admin.SearchOrderQuery
{
    public class SearchOrderQueryHandler : IRequestHandler<SearchOrderQueryRequest, OrderDto>
    {
        public async Task<OrderDto> Handle(SearchOrderQueryRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}