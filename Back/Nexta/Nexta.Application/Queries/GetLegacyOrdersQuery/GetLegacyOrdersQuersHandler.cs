using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQuersHandler : IRequestHandler<GetLegacyOrdersQueryRequest, Result<GetLegacyOrdersQueryResponse>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		public GetLegacyOrdersQuersHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}
		public async Task<Result<GetLegacyOrdersQueryResponse>> Handle(GetLegacyOrdersQueryRequest request, CancellationToken ct)
		{
			try
			{
				var legacyOrders = _mapper.Map<PagedData<Order>>(await _orderRepository.GetLegacyOrdersAsync(request.Filter, ct));

				return new Result<GetLegacyOrdersQueryResponse>(new GetLegacyOrdersQueryResponse(legacyOrders)).Success();
			}
			catch (Exception ex)
			{
				return new Result<GetLegacyOrdersQueryResponse>().BadRequest(ex.Message);
			}
		}
	}
}