using MediatR;
using Nexta.Application.DTO.Product;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommand : IRequest<ProductDto>
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}