using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Product;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandHandler : IRequestHandler<AddAdminProductToOrderCommand, ProductDto>
    {
        private readonly IValidator<AddAdminProductToOrderCommand> _validator;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public AddAdminProductToOrderCommandHandler(IValidator<AddAdminProductToOrderCommand> validator,
            IOrdersRepository ordersRepository, IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(AddAdminProductToOrderCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',' ,validationResult.Errors));

            var order = await _ordersRepository.GetAsyncByUserId(command.UserId, ct);
            order.AddProduct(command.ProductId, command.Count);

            var product = await _productsRepository.GetAsync(command.ProductId, ct);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
