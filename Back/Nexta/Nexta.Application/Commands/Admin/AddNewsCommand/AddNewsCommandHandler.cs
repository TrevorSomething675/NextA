using Nexta.Domain.Abstractions.Repositories;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Domain.Models.News;
using Nexta.Domain.Abstractions;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
	public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, News>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		
		public AddNewsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<News> Handle(AddNewsCommand request, CancellationToken ct = default)
		{
			throw new NotImplementedException();
			/*
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new ValidationException(string.Join(',', validationResult.Errors));

			var newsToCreate = _mapper.Map<News>(request);

			var news = await _newsRepository.AddAsync(newsToCreate);

			var result = _mapper.Map<NewsResponse>(news);

			return new AddNewsCommandResponse(result);
			*/
		}
	}
}