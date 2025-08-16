using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.RegistrationCommand
{
	public class RegistrationCommandHandler : IRequestHandler<RegistrationCommandRequest, RegistrationCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IHashService _passwordHashService;
		private readonly IValidator<RegistrationCommandRequest> _validator;
		public RegistrationCommandHandler(IUserRepository userRepository, IMapper mapper, 
			IHashService passwordHashService, IValidator<RegistrationCommandRequest> validator)
		{
			_passwordHashService = passwordHashService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<RegistrationCommandResponse> Handle(RegistrationCommandRequest request, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);

			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(", ", validationResult.Errors));

			var dbUser = await _userRepository.GetByEmailAsync(request.Email, ct);
			if (dbUser != null)
				throw new ConflictException("Такой пользователь уже существует");

			var passwordHash = _passwordHashService.Generate(request.Password);
			var user = _mapper.Map<User>(request);

			var userToCreate = CreateUserToRegister(user, passwordHash!);

			var response = _mapper.Map<UserResponse>(await _userRepository.AddAsync(userToCreate, ct));

			return new RegistrationCommandResponse(response);
		}

		private User CreateUserToRegister(User user, string passwordHash)
		{
			var userToCreate = new User
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				MiddleName = user.MiddleName,
				Email = user.Email,
				Phone = user.Phone,
				PasswordHash = passwordHash,
				Role = "User"
			};

			return userToCreate;
		}
	}
}