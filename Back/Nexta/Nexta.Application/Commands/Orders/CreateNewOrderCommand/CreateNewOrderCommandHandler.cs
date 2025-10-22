using Nexta.Domain.Specification;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Abstractions;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
	public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, CreateNewOrderCommandResponse>
	{
		private readonly IValidator<CreateNewOrderCommand> _validator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateNewOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateNewOrderCommand> validator)
		{
			_unitOfWork	= unitOfWork;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<CreateNewOrderCommandResponse> Handle(CreateNewOrderCommand command, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);

			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var order = new Order(command.UserId, Domain.Enums.OrderStatus.Accepted);
			order.ReplaceProducts(_mapper.Map<List<OrderItem>>(command.Products));

			var spec = new BasketByUserIdSpecification(command.UserId);

			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);
			basket.Clear();
			var updatedOrder = await _unitOfWork.Orders.AddAsync(order, ct);

			await _unitOfWork.SaveChangesAsync(ct);

            return new CreateNewOrderCommandResponse(order.Id);
		}
	}
}