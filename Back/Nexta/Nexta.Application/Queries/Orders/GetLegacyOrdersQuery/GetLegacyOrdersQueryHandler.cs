using Nexta.Application.DTO.Order;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Enums;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQueryHandler : IRequestHandler<GetLegacyOrdersQuery, PagedData<OrderDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetLegacyOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PagedData<OrderDto>> Handle(GetLegacyOrdersQuery query, CancellationToken ct)
		{
			var statuses = new OrderStatus[2] { OrderStatus.Complete, OrderStatus.Canceled };

            var spec = new OrderByStatusSpecification(
				query.Filter.UserId,
				statuses,
				query.Filter.PageNumber,
				query.Filter.PageSize
			);

			var orders = await _unitOfWork.Orders.GetPagedAsync(spec, ct);

			var response = _mapper.Map<PagedData<OrderDto>>(orders);
			
			return response;
		}
	}
}