using Nexta.Application.DTO.Orders;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
	public class DeleteProductFromOrderCommandHandler : IRequestHandler<DeleteProductFromOrderCommand, OrderItemDto>
	{
		private readonly IValidator<DeleteProductFromOrderCommand> _validator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DeleteProductFromOrderCommandHandler(IMapper mapper, IValidator<DeleteProductFromOrderCommand> validator, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<OrderItemDto> Handle(DeleteProductFromOrderCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(',', validationResult.Errors));

			var orders = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, ct);
			var deletetedProduct = orders.DeleteProduct(request.ProductId);
			_unitOfWork.Orders.Update(orders);

			var response = _mapper.Map<OrderItemDto>(deletetedProduct);

			return response;

        }
	}
}