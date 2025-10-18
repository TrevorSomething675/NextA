using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
	public class DeleteProductFromOrderCommandHandler : IRequestHandler<DeleteProductFromOrderCommand, DeleteProductFromOrderCommandResponse>
	{
		private readonly IValidator<DeleteProductFromOrderCommand> _validator;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteProductFromOrderCommandHandler(IValidator<DeleteProductFromOrderCommand> validator, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<DeleteProductFromOrderCommandResponse> Handle(DeleteProductFromOrderCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(',', validationResult.Errors));

			var orders = await _unitOfWork.Orders.GetAsync(request.OrderId, ct);
			orders.DeleteProduct(request.ProductId);
			_unitOfWork.Orders.Update(orders);

			return new DeleteProductFromOrderCommandResponse(request.OrderId, request.ProductId);
		}
	}
}