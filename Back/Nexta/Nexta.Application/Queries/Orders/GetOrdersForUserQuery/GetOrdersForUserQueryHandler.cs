using Nexta.Application.DTO.Order;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Enums;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
	public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, PagedData<OrderDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetOrdersForUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PagedData<OrderDto>> Handle(GetOrdersForUserQuery query, CancellationToken ct = default)
		{
			var spec = new OrderByStatusSpecification(
				query.Filter.UserId,
                [OrderStatus.Accepted, OrderStatus.InProgress, OrderStatus.Ready],
				query.Filter.PageNumber,
				query.Filter.PageSize);

			var orders = await _unitOfWork.Orders.GetPagedAsync(spec, ct);

			var response = _mapper.Map<PagedData<OrderDto>>(orders);

			return response;
		}
	}
}