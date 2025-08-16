using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using FluentValidation;
using Nexta.Domain.Exceptions;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
	public class AddNewsCommandHandler : IRequestHandler<AddNewsCommandRequest, AddNewsCommandResponse>
	{
		private readonly IValidator<AddNewsCommandRequest> _validator;
		private readonly INewsImageRepository _newsImageRepository;
		private readonly INewsRepository _newsRepository;
		private readonly IMapper _mapper;
		
		public AddNewsCommandHandler(INewsRepository newsRepository, INewsImageRepository newsImageRepository,

            IMapper mapper, IValidator<AddNewsCommandRequest> validator)
		{
			_newsImageRepository = newsImageRepository;
			_newsRepository = newsRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<AddNewsCommandResponse> Handle(AddNewsCommandRequest request, CancellationToken ct = default)
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