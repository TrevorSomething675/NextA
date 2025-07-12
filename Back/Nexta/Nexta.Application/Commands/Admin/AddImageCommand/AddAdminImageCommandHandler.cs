using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddImageCommand
{
	public class AddAdminImageCommandHandler : IRequestHandler<AddAdminImageCommandRequest, AddAdminImageCommandResponse>
	{
		private readonly IImageRepository _imageRepository;
		private readonly IMinioService _minioService;
		private readonly IMapper _mapper;

		public AddAdminImageCommandHandler(IImageRepository imageRepository, IMinioService minioService, IMapper mapper)
		{
			_imageRepository = imageRepository;
			_minioService = minioService;
			_mapper = mapper;
		}

		public async Task<AddAdminImageCommandResponse> Handle(AddAdminImageCommandRequest request, CancellationToken ct = default)
		{
			var image = _mapper.Map<Image>(request);

			var minioResult = await _minioService.AddFileAsync(image.Base64String, image.Name, image.Bucket, ct);
			var createImage = await _imageRepository.AddAsync(minioResult, ct);

			return new AddAdminImageCommandResponse(createImage.Id);
		}
	}
}