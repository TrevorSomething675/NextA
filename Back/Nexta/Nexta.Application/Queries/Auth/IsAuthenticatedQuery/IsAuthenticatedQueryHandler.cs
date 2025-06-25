using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsAuthenticatedQuery
{
	public class IsAuthenticatedQueryHandler : IRequestHandler<IsAuthenticatedQueryRequest, IsAuthenticatedQueryResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IValidator<IsAuthenticatedQueryRequest> _validator;

		public IsAuthenticatedQueryHandler(IUserRepository userRepository, IMapper mapper, 
			IValidator<IsAuthenticatedQueryRequest> validator, IJwtTokenService jwtTokenService)
		{
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<IsAuthenticatedQueryResponse> Handle(IsAuthenticatedQueryRequest request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if(!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var user = _mapper.Map<User>(await _userRepository.GetByEmailAsync(request.Email, ct));

			if(user == null)
				throw new NotFoundException("Пользователь не найден");

			var accessToken = _jwtTokenService.CreateAccessToken(request.Email, user.Role.ToString());

			return new IsAuthenticatedQueryResponse(user, accessToken);
		}
	}
}