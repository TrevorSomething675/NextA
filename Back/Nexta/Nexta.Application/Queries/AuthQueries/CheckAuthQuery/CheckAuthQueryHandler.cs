using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.AuthQueries.CheckAuthQuery
{
	public class CheckAuthQueryHandler : IRequestHandler<CheckAuthQueryRequest, CheckAuthQueryResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IValidator<CheckAuthQueryRequest> _validator;

		public CheckAuthQueryHandler(IUserRepository userRepository, IMapper mapper, 
			IValidator<CheckAuthQueryRequest> validator, IJwtTokenService jwtTokenService)
		{
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<CheckAuthQueryResponse> Handle(CheckAuthQueryRequest request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if(!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var user = _mapper.Map<User>(await _userRepository.GetAsync(request.UserId, ct));

			if(user == null)
				throw new NotFoundException("Пользователь не найден");

			var accessToken = _jwtTokenService.CreateAccessToken(request.UserId, user.Role.ToString());

			return new CheckAuthQueryResponse(user, accessToken);
		}
	}
}