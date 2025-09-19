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
		private readonly IUsersRepository _usersRepository;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IValidator<CheckAuthCommand> _validator;

		public CheckAuthCommandHandler(IUsersRepository usersRepository, IMapper mapper, 
			IValidator<CheckAuthCommand> validator, IJwtTokenService jwtTokenService)
		{
			_jwtTokenService = jwtTokenService;
			_usersRepository = usersRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<CheckAuthCommandResponse> Handle(CheckAuthCommand command, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);
			if(!validationResult.IsValid)
				throw new ValidationException(string.Join(", ", validationResult.Errors));

			var user = await _usersRepository.GetByEmailAsync(command.Email, ct);

			if(user == null)
				throw new UnauthorizedAccessException("Пользователь не найден");

			var accessToken = _jwtTokenService.CreateAccessToken(command.Email, user.Role.ToString());

			var userResponse = _mapper.Map<UserResponse>(user);

			return new CheckAuthCommandResponse(userResponse, accessToken);
		}
	}
}