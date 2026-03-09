using Nexta.Application.Abstractions;
using Nexta.Application.DTO.Orders;
using Nexta.Domain.Specification;
using Nexta.Domain.Enums;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Products;

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
				query.UserId,
                [OrderStatus.Accepted, OrderStatus.InProgress, OrderStatus.Ready],
				query.PageNumber,
				query.PageSize
			);

			var orders = _mapper.Map<PagedData<OrderDto>>(await _unitOfWork.Orders.GetAsync(spec, ct));

			foreach (var order in orders?.Items)
			{
				var productIds = order.Products.Select(p => p.ProductId).ToList();
				var products = await _unitOfWork.Products.GetByIdsAsync(productIds, ct);

				foreach (var product in order.Products)
				{
					product.Product = _mapper.Map<ProductDto>(products.Find(p => p.Id == product.ProductId));
				}
			}

			var response = _mapper.Map<PagedData<OrderDto>>(orders);

			return response;
		}
	}
}