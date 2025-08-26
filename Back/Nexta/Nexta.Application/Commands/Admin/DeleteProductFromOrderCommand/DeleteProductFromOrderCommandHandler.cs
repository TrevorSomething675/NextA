using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
	public class DeleteProductFromOrderCommandHandler : IRequestHandler<DeleteProductFromOrderCommand, DeleteProductFromOrderCommandResponse>
	{
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IValidator<DeleteProductFromOrderCommand> _validator;

		public DeleteProductFromOrderCommandHandler(IOrderProductRepository orderProductRepository, IValidator<DeleteProductFromOrderCommand> validator)
		{
			_orderProductRepository = orderProductRepository;
			_validator = validator;
		}

		public async Task<DeleteProductFromOrderCommandResponse> Handle(DeleteProductFromOrderCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(',', validationResult.Errors));

			var deletedOrderProduct = await _orderProductRepository.DeleteAsync(request.OrderId, request.ProductId, ct);

			return new DeleteProductFromOrderCommandResponse(deletedOrderProduct.OrderId, deletedOrderProduct.ProductId);
		}
	}
}