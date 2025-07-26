using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminDetailToOrderCommand
{
    public class AddAdminDetailToOrderCommandHandler : IRequestHandler<AddAdminDetailToOrderCommandRequest, AddAdminDetailToOrderCommandResponse>
    {
        private readonly IValidator<AddAdminDetailToOrderCommandRequest> _validator;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public AddAdminDetailToOrderCommandHandler(IValidator<AddAdminDetailToOrderCommandRequest> validator, IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddAdminDetailToOrderCommandResponse> Handle(AddAdminDetailToOrderCommandRequest request, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',' ,validationResult.Errors));

            var detailToAdd = new OrderDetail
            {
                OrderId = request.OrderId,
                DetailId = request.DetailId,
                Count = request.Count
            };

            var createdOrderDetail = await _orderDetailRepository.AddAsync(detailToAdd, ct);
            if (createdOrderDetail == null)
                throw new BadRequestException("Не удалось добавить деталь");

            var response = _mapper.Map<OrderDetailResponse>(createdOrderDetail);

            return new AddAdminDetailToOrderCommandResponse(response);
        }
    }
}
