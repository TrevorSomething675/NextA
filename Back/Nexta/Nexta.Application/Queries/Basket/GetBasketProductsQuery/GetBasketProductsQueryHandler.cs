using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
	public class GetBasketProductsQueryHandler : IRequestHandler<GetBasketProductsQuery, GetBasketProductsQueryResponse>
	{
		private readonly IBasketProductRepository _basketProductRepository;
		private readonly IMapper _mapper;

		public GetBasketProductsQueryHandler(IBasketProductRepository basketProductRepository, IMapper mapper)
		{
			_basketProductRepository = basketProductRepository;
			_mapper = mapper;
		}

		public async Task<GetBasketProductsQueryResponse> Handle(GetBasketProductsQuery query, CancellationToken ct = default)
		{
			var products = _mapper.Map<List<BasketProductResponse>>(await _basketProductRepository.GetAllAsync(query.UserId, ct));

			return new GetBasketProductsQueryResponse(products);
		}
	}
}