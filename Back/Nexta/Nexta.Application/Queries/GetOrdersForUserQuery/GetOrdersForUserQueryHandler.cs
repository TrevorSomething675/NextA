using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using Nexta.Domain.Enums;
using Nexta.Domain.Filters;

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

		public async Task<GetOrdersForUserQueryResponse> Handle(GetOrdersForUserQueryRequest request, CancellationToken ct = default)
		{
			request.Filter.Statuses = GetOrderStatuses();

			var orders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetOrdersAsync(request.Filter, ct));
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