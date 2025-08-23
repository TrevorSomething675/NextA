using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
	public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommandRequest, CreateNewOrderCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IValidator<CreateNewOrderCommandRequest> _validator;

		public CreateNewOrderCommandHandler(IUserDetailRepository userDetailRepository, IOrderDetailRepository orderDetailRepository,
			IMapper mapper, IOrderRepository orderRepository, IValidator<CreateNewOrderCommandRequest> validator)
		{
			_orderDetailRepository = orderDetailRepository;
			_userDetailRepository = userDetailRepository;
			_orderRepository = orderRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<CreateNewOrderCommandResponse> Handle(CreateNewOrderCommandRequest request, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);

			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var userDetails = await _userDetailRepository.GetRangeAsync(request.UserId, request.DetailIds, ct);
			var order = await _orderRepository.AddAsync(new Order { UserId = request.UserId, CreatedDate = request.CreatedDate });

			var orderDetails = new List<OrderDetail>();

			foreach (var detail in userDetails)
			{
				orderDetails.Add(new OrderDetail { OrderId = order.Id, DetailId = detail.DetailId, Count = detail.Count.Value });
			}

			await _orderDetailRepository.AddRangeAsync(orderDetails, ct);
			await _userDetailRepository.DeleteRangeAsync(request.UserId, request.DetailIds, ct);

			return new CreateNewOrderCommandResponse(order.Id);
		}
	}
}