using Nexta.Application.Abstractions;
using Nexta.Domain.Specification;
using MediatR;

namespace Nexta.Application.Commands.Baskets.DeleteBasketProductCommand
{
	public class DeleteBasketProductCommandHandler : IRequestHandler<DeleteBasketProductCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteBasketProductCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Guid> Handle(DeleteBasketProductCommand command, CancellationToken ct)
		{
			var spec = new BasketByUserIdSpecification(command.UserId);

			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);
            var productId = basket.RemoveProduct(command.ProductId);
			_unitOfWork.Baskets.Update(basket);

			await _unitOfWork.SaveChangesAsync(ct);

			return productId;
		}
	}
}