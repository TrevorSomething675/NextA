using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
	public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, CreateNewOrderCommandResponse>
	{
		private readonly IBasketProductRepository _basketProductRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IValidator<CreateNewOrderCommand> _validator;

		public CreateNewOrderCommandHandler(IBasketProductRepository basketProductRepository, IOrderProductRepository orderProductRepository,
			IOrderRepository orderRepository, IValidator<CreateNewOrderCommand> validator)
		{
            _orderProductRepository = orderProductRepository;
            _basketProductRepository = basketProductRepository;
			_orderRepository = orderRepository;
			_validator = validator;
		}

		public async Task<CreateNewOrderCommandResponse> Handle(CreateNewOrderCommand command, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);

			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var basketProducts = await _basketProductRepository.GetRangeAsync(command.UserId, command.ProductIds, ct);
			var order = await _orderRepository.AddAsync(new Order { UserId = command.UserId, CreatedDate = command.CreatedDate });

			var orderProducts = new List<OrderProduct>();

			foreach (var product in basketProducts)
			{
                orderProducts.Add(new OrderProduct { OrderId = order.Id, ProductId = product.ProductId, Count = product.Count.Value });
			}

			await Task.WhenAll(
				_orderProductRepository.AddRangeAsync(orderProducts, ct), 
				_basketProductRepository.DeleteRangeAsync(command.UserId, command.ProductIds, ct));

			return new CreateNewOrderCommandResponse(order.Id);
		}
	}
}