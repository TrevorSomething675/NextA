using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.RegistrationCommand
{
	public class RegistrationCommandHandler : IRequestHandler<RegistrationCommandRequest, RegistrationCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IPasswordHashService _passwordHashService;
		public RegistrationCommandHandler(IJwtTokenService jwtTokenService, IUserRepository userRepository, 
			IMapper mapper, IPasswordHashService passwordHashService)
		{
			_passwordHashService = passwordHashService;
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_mapper = mapper;
		}
		public async Task<RegistrationCommandResponse> Handle(RegistrationCommandRequest request, CancellationToken ct)
		{
			var user = await _userRepository.GetByEmailAsync(request.Email, ct);
			if (user != null)
				throw new ConflictException("Такой пользователь уже существует");

			var passwordHash = _passwordHashService.Generate(request.Password);

			var userToCreate = _mapper.Map<UserEntity>(request);
				userToCreate.PasswordHash = passwordHash!;
				userToCreate.Role = Role.User;

			var createdUser = _mapper.Map<User>(await _userRepository.AddAsync(userToCreate, ct));

			var accessToken = _jwtTokenService.CreateAccessToken(createdUser.Id, userToCreate.Role.ToString());

			return new RegistrationCommandResponse(createdUser, accessToken, "refreshToken");
		}
	}
}