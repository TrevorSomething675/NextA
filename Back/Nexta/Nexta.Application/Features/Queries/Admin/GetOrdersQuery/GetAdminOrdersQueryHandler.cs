using Nexta.Application.DTO.Orders;
using Nexta.Domain.Specification;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryHandler : IRequestHandler<GetAdminOrdersQuery, PagedData<OrderDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAdminOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PagedData<OrderDto>> Handle(GetAdminOrdersQuery query, CancellationToken ct = default)
		{
			var spec = new OrderByUserDataSpecification(query.SearchTerm, query.PageNumber, query.PageSize);

			var orders = await _unitOfWork.Orders.GetOrdersByFullNameAsync(spec, ct);
			var response = _mapper.Map<PagedData<OrderDto>>(orders);

			return response;
		}
	}
}