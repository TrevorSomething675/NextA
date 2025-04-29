using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.RegistrationCommand
{
	public class RegistrationCommandHandler : IRequestHandler<RegistrationCommandRequest, Result<RegistrationCommandResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IUserRepository _userRepository;
		private readonly IPasswordHashService _passwordHashService;
		public RegistrationCommandHandler(IJwtTokenService jwtTokenService, IUserRepository userRepository, 
			IMapper mapper, IPasswordHashService passwordHashService)
		{
			_passwordHashService = passwordHashService;
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_mapper = mapper;
		}
		public async Task<Result<RegistrationCommandResponse>> Handle(RegistrationCommandRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userRepository.GetByEmailAsync(request.Email);
				if (user != null)
					return new Result<RegistrationCommandResponse>().Invalid("Такой пользователь уже существует");

				var passwordHash = _passwordHashService.Generate(request.Password);

				var userToCreate = _mapper.Map<UserEntity>(request);
					userToCreate.PasswordHash = passwordHash!;
					userToCreate.Role = Role.User;

				var createdUser = _mapper.Map<User>(await _userRepository.AddAsync(userToCreate));

				var accessToken = _jwtTokenService.CreateAccessToken(createdUser.Id, userToCreate.Role.ToString());

				return new Result<RegistrationCommandResponse>(new RegistrationCommandResponse(createdUser, accessToken, "refreshToken")).Success();
			}
			catch(Exception ex)
			{
				return new Result<RegistrationCommandResponse>().Invalid(ex.Message);
			}
		}
	}
}