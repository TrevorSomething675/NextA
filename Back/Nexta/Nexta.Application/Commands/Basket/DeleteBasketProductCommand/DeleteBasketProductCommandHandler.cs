using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketProductCommand
{
	public class DeleteBasketProductCommandHandler : IRequestHandler<DeleteBasketProductCommand, DeleteBasketProductCommandResponse>
	{
		private readonly IBasketProductRepository _basketProductRepository;

		public DeleteBasketProductCommandHandler(IBasketProductRepository basketProductRepository)
		{
			_basketProductRepository = basketProductRepository;
		}

		public async Task<DeleteBasketProductCommandResponse> Handle(DeleteBasketProductCommand request, CancellationToken ct)
		{
			var basketProduct = await _basketProductRepository.GetAsync(request.UserId, request.ProductId, ct);
			if (basketProduct == null)
				throw new NotFoundException("В корзине нет такой детали");

			var deletedBasketProduct = await _basketProductRepository.DeleteAsync(basketProduct, ct);

			return new DeleteBasketProductCommandResponse(deletedBasketProduct.UserId, deletedBasketProduct.ProductId);
		}
	}
}