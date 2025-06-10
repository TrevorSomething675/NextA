using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetOrdersQuery
{
	public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQueryRequest, GetOrdersForUserQueryResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetOrdersForUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetOrdersForUserQueryResponse> Handle(GetOrdersForUserQueryRequest request, CancellationToken ct)
		{
			var orders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetOrdersAsync(request.Filter, ct));
			var totalCount = await _orderRepository.CountOrdersAsync(request.Filter.UserId, ct);

			return new GetOrdersForUserQueryResponse(orders, totalCount);
		}
	}
}