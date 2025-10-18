using Nexta.Domain.Models.Product;
using Nexta.Domain.Abstractions;
using MediatR;

namespace Nexta.Application.Commands.Admin.CreateAdminProductCommand
{
    public class CreateAdminProductCommandHandler : IRequestHandler<CreateAdminProductCommand, CreateAdminProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdminProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateAdminProductCommandResponse> Handle(CreateAdminProductCommand command, CancellationToken ct = default)
        {
            var product = new Product(
                    command.Name,
                    command.Article,
                    command.Description,
                    command.Count,
                    command.NewPrice,
                    command.OldPrice,
                    command.IsVisible
                );
            
            var createdProduct = await _unitOfWork.Products.AddAsync(product, ct);

            return new CreateAdminProductCommandResponse(createdProduct.Id);
        }
    }
}