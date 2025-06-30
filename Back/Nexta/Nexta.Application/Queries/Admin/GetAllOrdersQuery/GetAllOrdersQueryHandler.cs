using Nexta.Domain.Abstractions.Repositories;
using AutoMapper;
using MediatR;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken ct = default)
		{
			var pagedOrders = _mapper.Map<PagedData<OrderResponse>>(await _orderRepository.GetAllOrdersAsync(request.Filter, ct));

			return new GetAllOrdersQueryResponse(pagedOrders);
		}
	}
}