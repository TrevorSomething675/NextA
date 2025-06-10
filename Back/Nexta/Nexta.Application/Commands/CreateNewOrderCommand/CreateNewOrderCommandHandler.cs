using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using Nexta.Domain.Entities;
using FluentValidation;

namespace Nexta.Application.Commands.CreateNewOrderCommand
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

			var userDetails = _mapper.Map<List<UserDetail>>(await _userDetailRepository.GetRangeAsync(request.UserId, request.DetailIds, ct));
			var order = _mapper.Map<Order>(await _orderRepository.AddAsync(new OrderEntity{UserId = request.UserId}));
			var orderDetails = new List<OrderDetailEntity>();

			foreach (var detail in userDetails)
			{
				orderDetails.Add(new OrderDetailEntity { OrderId = order.Id, DetailId = detail.DetailId, Count = detail.Count });
			}

			var createdOrderDetails = _mapper.Map<List<OrderDetail>>(await _orderDetailRepository.AddRangeAsync(orderDetails, ct));
			var deletedUserDetails = _mapper.Map<List<UserDetail>>(await _userDetailRepository.DeleteRangeAsync(request.UserId, request.DetailIds, ct));

			var createdOrder = _mapper.Map<Order>(await _orderRepository.GetOrderAsync(order.Id, ct));

			return new CreateNewOrderCommandResponse(createdOrder);
		}
	}
}