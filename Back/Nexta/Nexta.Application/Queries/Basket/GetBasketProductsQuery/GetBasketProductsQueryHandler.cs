using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
	public class GetBasketProductsQueryHandler : IRequestHandler<GetBasketProductsQuery, GetBasketProductsQueryResponse>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public GetBasketProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<GetBasketProductsQueryResponse> Handle(GetBasketProductsQuery query, CancellationToken ct = default)
		{
			var products = _mapper.Map<List<ProductResponse>>(await _productRepository.GetBasketProductsAsync(query.Filter, ct));

			return new GetBasketProductsQueryResponse(products);
		}
	}
}