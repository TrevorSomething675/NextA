using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO.Response;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Auth.CheckAuthCommand
{
	public class CheckAuthCommandHandler : IRequestHandler<CheckAuthCommand, CheckAuthCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IValidator<CheckAuthCommand> _validator;

		public CheckAuthCommandHandler(IUserRepository userRepository, IMapper mapper, 
			IValidator<CheckAuthCommand> validator, IJwtTokenService jwtTokenService)
		{
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<CheckAuthCommandResponse> Handle(CheckAuthCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if(!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var user = await _userRepository.GetByEmailAsync(request.Email, ct);

			if(user == null)
				throw new UnauthorizedAccessException("Пользователь не найден");

			var accessToken = _jwtTokenService.CreateAccessToken(request.Email, user.Role.ToString());

			var userResponse = _mapper.Map<UserResponse>(user);

			return new CheckAuthCommandResponse(userResponse, accessToken);
		}
	}
}