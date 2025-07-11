using Nexta.Domain.Abstractions.Repositories;
using MediatR;
using AutoMapper;
using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Basket.UpdateBasketDetailCommand
{
	public class UpdateBasketDetailCommandHandler : IRequestHandler<UpdateBasketDetailCommandRequest, UpdateBasketDetailCommandResponse>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IMapper _mapper;

		public UpdateBasketDetailCommandHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}

		public async Task<UpdateBasketDetailCommandResponse> Handle(UpdateBasketDetailCommandRequest request, CancellationToken cancellationToken)
		{
			var userDetail = _mapper.Map<UserDetail>(request);
			var updatedUserDetail = await _userDetailRepository.UpdateAsync(userDetail);
			var response = _mapper.Map<UpdateBasketDetailCommandResponse>(updatedUserDetail);

			return response;
		}
	}
}
