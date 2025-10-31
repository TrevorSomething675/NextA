using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Commands.Orders.UpdateOrderStatusCommand
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken ct = default)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, ct);
            order.UpdateStatus(request.Status);

            await _unitOfWork.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}