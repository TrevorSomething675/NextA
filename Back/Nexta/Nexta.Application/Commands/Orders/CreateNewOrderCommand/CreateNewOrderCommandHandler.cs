using Nexta.Domain.Models.Order;
using Nexta.Domain.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.VisualBasic;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
	public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, CreateNewOrderCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<CreateNewOrderCommand> _validator;

		public CreateNewOrderCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateNewOrderCommand> validator)
		{
			_unitOfWork	= unitOfWork;
			_validator = validator;
		}

		public async Task<CreateNewOrderCommandResponse> Handle(CreateNewOrderCommand command, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);

			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var order = new Order(command.UserId);
			foreach (var productId in command.ProductIds)
			{
				order.AddProduct(productId);
			}

			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(command.UserId, ct);
			basket.Clear();
			var updatedOrder = await _unitOfWork.Orders.AddAsync(order, ct);

			await _unitOfWork.SaveChangesAsync(ct);

            return new CreateNewOrderCommandResponse(order.Id);
		}
	}
}