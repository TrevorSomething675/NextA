using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;
using Nexta.Domain.Base;
using Nexta.Domain.Models.Order;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryHandler : IRequestHandler<GetAdminOrdersQuery, GetAdminOrdersQueryResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetAdminOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminOrdersQueryResponse> Handle(GetAdminOrdersQuery query, CancellationToken ct = default)
		{
			var pagedOrders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetOrdersAsync(query.Filter, ct));
			var responseOrders = _mapper.Map<PagedData<OrderResponse>>(pagedOrders);

			return new GetAdminOrdersQueryResponse(responseOrders);
		}
	}
}