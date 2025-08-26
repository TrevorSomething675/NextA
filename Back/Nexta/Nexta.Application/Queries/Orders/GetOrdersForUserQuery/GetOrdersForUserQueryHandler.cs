using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
	public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, GetOrdersForUserQueryResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetOrdersForUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetOrdersForUserQueryResponse> Handle(GetOrdersForUserQuery query, CancellationToken ct = default)
		{
			query.Filter.Statuses = GetOrderStatuses();

			var orders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetOrdersAsync(query.Filter, ct));
			var totalCount = await _orderRepository.CountOrdersAsync(query.Filter.UserId, ct);

			return new GetOrdersForUserQueryResponse(orders, totalCount);
		}

		private List<OrderStatus> GetOrderStatuses()
		{
			var statuses = new List<OrderStatus>
			{
				OrderStatus.Accepted,
				OrderStatus.InProgress,
				OrderStatus.Canceled,
				OrderStatus.Ready
			};

			return statuses;
		}
	}
}