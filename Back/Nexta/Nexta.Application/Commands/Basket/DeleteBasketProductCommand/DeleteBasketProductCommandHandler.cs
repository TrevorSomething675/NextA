using Nexta.Domain.Abstractions;
using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketProductCommand
{
	public class DeleteBasketProductCommandHandler : IRequestHandler<DeleteBasketProductCommand, DeleteBasketProductCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteBasketProductCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<DeleteBasketProductCommandResponse> Handle(DeleteBasketProductCommand command, CancellationToken ct)
		{
			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(command.UserId, ct);
            basket.RemoveProduct(command.ProductId);
			var result = _unitOfWork.Baskets.Update(basket);

			await _unitOfWork.SaveChangesAsync(ct);

			return new DeleteBasketProductCommandResponse(result.Id);
		}
	}
}