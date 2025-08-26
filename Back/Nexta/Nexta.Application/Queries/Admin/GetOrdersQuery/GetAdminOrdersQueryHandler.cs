using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;

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

		public async Task<GetAdminOrdersQueryResponse> Handle(GetAdminOrdersQuery request, CancellationToken ct = default)
		{
			var pagedOrders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetAllOrdersAsync(request.Filter, ct));

			return new GetAdminOrdersQueryResponse(pagedOrders);
		}
	}
}