using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.CreateAdminProductCommand
{
    public class CreateAdminProductCommandHandler : IRequestHandler<CreateAdminProductCommand, CreateAdminProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CreateAdminProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateAdminProductCommandResponse> Handle(CreateAdminProductCommand command, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(command);
            var createdProductId = await _productRepository.CreateAsync(product, ct);

            return new CreateAdminProductCommandResponse(createdProductId);
        }
    }
}