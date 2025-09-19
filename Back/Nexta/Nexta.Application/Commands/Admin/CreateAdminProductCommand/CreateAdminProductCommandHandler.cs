using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.CreateAdminProductCommand
{
    public class CreateAdminProductCommandHandler : IRequestHandler<CreateAdminProductCommand, CreateAdminProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;

        public CreateAdminProductCommandHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<CreateAdminProductCommandResponse> Handle(CreateAdminProductCommand command, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(command);
            var createdProductId = await _productsRepository.CreateAsync(product, ct);

            return new CreateAdminProductCommandResponse(createdProductId);
        }
    }
}