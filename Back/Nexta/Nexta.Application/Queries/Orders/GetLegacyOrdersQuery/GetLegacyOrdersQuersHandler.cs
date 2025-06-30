using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQuersHandler : IRequestHandler<GetLegacyOrdersQueryRequest, GetLegacyOrdersQueryResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetLegacyOrdersQuersHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetLegacyOrdersQueryResponse> Handle(GetLegacyOrdersQueryRequest request, CancellationToken ct)
		{
			request.Filter.Statuses = GetLegacyOrderStatuses();

			var legacyOrders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetOrdersAsync(request.Filter, ct));

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