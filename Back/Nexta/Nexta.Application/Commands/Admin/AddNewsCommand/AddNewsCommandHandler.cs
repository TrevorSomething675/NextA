using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
	public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, AddNewsCommandResponse>
	{
		private readonly IValidator<AddNewsCommand> _validator;
		private readonly INewsRepository _newsRepository;
		private readonly IMapper _mapper;
		
		public AddNewsCommandHandler(INewsRepository newsRepository, IMapper mapper, IValidator<AddNewsCommand> validator)
		{
			_newsRepository = newsRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<AddNewsCommandResponse> Handle(AddNewsCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(',', validationResult.Errors));

			var newsToCreate = _mapper.Map<News>(request);

			var news = await _newsRepository.AddAsync(newsToCreate);

			var result = _mapper.Map<NewsResponse>(news);

			return new AddNewsCommandResponse(result);
		}
	}
}