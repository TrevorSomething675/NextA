using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;
using Nexta.Domain.Base;
using Nexta.Domain.Models.Order;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
	public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, GetOrdersForUserQueryResponse>
	{
		private readonly IOrderRepositoryL _orderRepository;
		private readonly IMapper _mapper;

		public GetOrdersForUserQueryHandler(IOrderRepositoryL orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetOrdersForUserQueryResponse> Handle(GetOrdersForUserQuery query, CancellationToken ct = default)
		{
			query.Filter.Statuses = GetOrderStatuses();

			var orders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetOrdersAsync(query.Filter, ct));
			var totalCount = await _orderRepository.CountOrdersAsync(query.Filter.UserId!.Value, ct);

			var responseOrders = _mapper.Map<PagedData<OrderResponse>>(orders);

			return new GetOrdersForUserQueryResponse(responseOrders, totalCount);
		}

		private List<OrderStatus> GetOrderStatuses()
		{
			var statuses = new List<OrderStatus>
			{
				OrderStatus.Accepted,
				OrderStatus.InProgress,
				OrderStatus.Ready
			};

			return statuses;
		}
	}
}