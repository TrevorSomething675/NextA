using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Basket;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
	public class GetBasketProductsQueryHandler : IRequestHandler<GetBasketProductsQuery, BasketDto>
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IProductsRepositoryL _productsRepository;
		private readonly IMapper _mapper;

		public GetBasketProductsQueryHandler(IBasketRepository basketRepository, IProductsRepositoryL productsRepository, IMapper mapper)
		{
			_productsRepository = productsRepository;
            _basketRepository = basketRepository;
			_mapper = mapper;
		}

		public async Task<BasketDto> Handle(GetBasketProductsQuery query, CancellationToken ct = default)
		{
			var basket = await _basketRepository.GetByUserIdAsync(query.UserId, ct);
			var response = _mapper.Map<BasketDto>(basket);

			return response;
		}
	}
}