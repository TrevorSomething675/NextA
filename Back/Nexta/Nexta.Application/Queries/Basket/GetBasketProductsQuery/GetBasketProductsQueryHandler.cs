using Nexta.Application.DTO.Products;
using Nexta.Application.DTO.Baskets;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
	public class GetBasketProductsQueryHandler : IRequestHandler<GetBasketProductsQuery, BasketDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetBasketProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BasketDto> Handle(GetBasketProductsQuery query, CancellationToken ct = default)
		{
			var spec = new BasketByUserIdSpecification(query.UserId);
			var basketDto = _mapper.Map<BasketDto>(await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct));

			var productIds = basketDto.Products.Select(p => p.ProductId).ToList();
			var productDtos = _mapper.Map<List<ProductDto>>(await _unitOfWork.Products.GetByIdsAsync(productIds, ct));

			var basketWithProducts = basketDto with
			{
				Products = basketDto.Products.Select(i => i with
				{
					Product = productDtos.FirstOrDefault(p => p.Id == i.ProductId)
				})
				.ToList()
				.AsReadOnly()
			};

            return basketWithProducts;
		}
	}
}