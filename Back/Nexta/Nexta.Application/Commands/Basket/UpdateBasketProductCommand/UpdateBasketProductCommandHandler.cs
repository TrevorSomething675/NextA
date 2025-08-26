using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.UpdateBasketProductCommand
{
	public class UpdateBasketProductCommandHandler : IRequestHandler<UpdateBasketProductCommand, UpdateBasketProductCommandResponse>
	{
		private readonly IBasketProductRepository _basketProductRepository;
		private readonly IMapper _mapper;

		public UpdateBasketProductCommandHandler(IBasketProductRepository basketProductRepository, IMapper mapper)
		{
			_basketProductRepository = basketProductRepository;
			_mapper = mapper;
		}

		public async Task<UpdateBasketProductCommandResponse> Handle(UpdateBasketProductCommand command, CancellationToken ct )
		{
			var basketProduct = _mapper.Map<BasketProduct>(command);
			var updatedBasketProduct = await _basketProductRepository.UpdateAsync(basketProduct, ct);
			var result = _mapper.Map<UpdateBasketProductCommandResponse>(updatedBasketProduct);

			return result;
		}
	}
}
