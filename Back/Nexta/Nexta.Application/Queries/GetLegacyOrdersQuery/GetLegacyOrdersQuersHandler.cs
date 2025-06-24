using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using Nexta.Domain.Enums;

namespace Nexta.Application.Queries.GetLegacyOrdersQuery
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
			request.Filter.Statuses = GetOrderStatuses();

			var legacyOrders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetOrdersAsync(request.Filter, ct));

			return new GetLegacyOrdersQueryResponse(legacyOrders);
		}
		private List<OrderStatus> GetOrderStatuses()
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