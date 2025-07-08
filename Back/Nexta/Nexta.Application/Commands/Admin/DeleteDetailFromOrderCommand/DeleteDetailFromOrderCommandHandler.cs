using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteDetailFromOrderCommand
{
	public class DeleteDetailFromOrderCommandHandler : IRequestHandler<DeleteDetailFromOrderCommandRequest, DeleteDetailFromOrderCommandResponse>
	{
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IValidator<DeleteDetailFromOrderCommandRequest> _validator;

		public DeleteDetailFromOrderCommandHandler(IOrderDetailRepository orderDetailRepository, IValidator<DeleteDetailFromOrderCommandRequest> validator)
		{
			_orderDetailRepository = orderDetailRepository;
			_validator = validator;
		}

		public async Task<DeleteDetailFromOrderCommandResponse> Handle(DeleteDetailFromOrderCommandRequest request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(',', validationResult.Errors));

			var deletedOrderDetail = await _orderDetailRepository.DeleteAsync(request.OrderId, request.DetailId, ct);

			return new DeleteDetailFromOrderCommandResponse(deletedOrderDetail.OrderId, deletedOrderDetail.DetailId);
		}
	}
}