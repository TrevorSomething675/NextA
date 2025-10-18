using Nexta.Domain.Abstractions;
using MediatR;

namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Guid> Handle(DeleteOrderCommand command, CancellationToken ct)
		{
			var order = await _unitOfWork.Orders.GetAsync(command.OrderId, ct);
			_unitOfWork.Orders.Delete(order);
			await _unitOfWork.SaveChangesAsync(ct);

			return order.Id;
		}
	}
}