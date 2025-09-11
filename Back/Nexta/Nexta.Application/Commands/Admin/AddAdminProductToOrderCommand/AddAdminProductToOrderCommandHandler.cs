using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandHandler : IRequestHandler<AddAdminProductToOrderCommand, AddAdminProductToOrderCommandResponse>
    {
        private readonly IValidator<AddAdminProductToOrderCommand> _validator;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IMapper _mapper;

        public AddAdminProductToOrderCommandHandler(IValidator<AddAdminProductToOrderCommand> validator, 
            IOrderProductRepository orderProductRepository, IMapper mapper)
        {
            _orderProductRepository = orderProductRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddAdminProductToOrderCommandResponse> Handle(AddAdminProductToOrderCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',' ,validationResult.Errors));

            var productToAdd = new OrderProduct
            {
                OrderId = command.OrderId,
                ProductId = command.ProductId,
                Count = command.Count
            };

            var createdOrderProduct = await _orderProductRepository.AddAsync(productToAdd, ct);
            if (createdOrderProduct == null)
                throw new BadRequestException("Не удалось добавить деталь");

            var result = _mapper.Map<OrderProductResponse>(createdOrderProduct);

            return new AddAdminProductToOrderCommandResponse(result);
        }
    }
}
