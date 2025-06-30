using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
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

		public async Task<GetOrdersForUserQueryResponse> Handle(GetOrdersForUserQueryRequest request, CancellationToken ct = default)
		{
			request.Filter.Statuses = GetOrderStatuses();

			var orders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetOrdersAsync(request.Filter, ct));
			var totalCount = await _orderRepository.CountOrdersAsync(request.Filter.UserId, ct);

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