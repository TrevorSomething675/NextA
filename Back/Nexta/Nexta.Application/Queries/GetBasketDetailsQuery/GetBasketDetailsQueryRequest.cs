using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetUserBasketDetails
{
    public class GetBasketDetailsQueryRequest : IRequest<Result<GetBasketDetailsQueryResponse>>
    {
		public BasketDetailsFilter Filter { get; set; } = null!;
    }
}