using Nexta.Application.DTO.Basket;
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

			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);

			var response = _mapper.Map<BasketDto>(basket);

			return response;
		}
	}
}