using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetWarehouseDetailsQuery
{
    public class GetWarehouseDetailsQueryRequest : IRequest<Result<GetWarehouseDetailsQueryResponse>>
    {
        public BaseFilter Filter { get; set; } = null!;
    }
}
