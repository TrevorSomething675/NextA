using Nexta.Application.DTO.Order;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryHandler : IRequestHandler<GetAdminOrdersQuery, GetAdminOrdersQueryResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAdminOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<GetAdminOrdersQueryResponse> Handle(GetAdminOrdersQuery query, CancellationToken ct = default)
		{
			var spec = new OrderByUserDataSpecification(query.Filter.SearchTerm, query.Filter.PageNumber, query.Filter.PageSize);

			var orders = await _unitOfWork.Orders.GetOrdersByFullNameAsync(spec, ct);
			var response = _mapper.Map<PagedData<OrderDto>>(orders);

			return new GetAdminOrdersQueryResponse(response);
		}
	}
}