using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
    public class GetAdminOrdersQuery : IRequest<GetAdminOrdersQueryResponse>
    {
        public GetOrdersFilter Filter { get; set; }
    }
}