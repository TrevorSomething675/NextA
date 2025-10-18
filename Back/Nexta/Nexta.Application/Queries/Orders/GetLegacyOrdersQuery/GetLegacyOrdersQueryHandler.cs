using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQueryHandler : IRequestHandler<GetLegacyOrdersQuery, GetLegacyOrdersQueryResponse>
	{
		private readonly IOrderRepositoryL _orderRepository;
		private readonly IMapper _mapper;

		public GetLegacyOrdersQueryHandler(IOrderRepositoryL orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetLegacyOrdersQueryResponse> Handle(GetLegacyOrdersQuery query, CancellationToken ct)
		{
			query.Filter.Statuses = GetLegacyOrderStatuses();

			var legacyOrders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetOrdersAsync(query.Filter, ct));

			return new GetLegacyOrdersQueryResponse(legacyOrders);
		}

		private List<OrderStatus> GetLegacyOrderStatuses()
		{
			var statuses = new List<OrderStatus>
			{
				OrderStatus.Canceled,
				OrderStatus.Complete
			};

			return statuses;
		}
	}
}