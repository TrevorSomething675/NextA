using Nexta.Application.DTO.Product;
using Nexta.Domain.Abstractions;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandHandler : IRequestHandler<AddAdminProductToOrderCommand, ProductDto>
    {
        private readonly IValidator<AddAdminProductToOrderCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddAdminProductToOrderCommandHandler(IValidator<AddAdminProductToOrderCommand> validator,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(AddAdminProductToOrderCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',' ,validationResult.Errors));

            var order = await _unitOfWork.Orders.GetByIdAsync(command.UserId, ct);
            order.AddProduct(command.ProductId, command.Count);
            await _unitOfWork.SaveChangesAsync(ct);

            var product = await _unitOfWork.Products.GetByIdAsync(command.ProductId, ct);

            return _mapper.Map<ProductDto>(product);
        }
    }
}