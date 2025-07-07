using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
	public class UpdateAdminDetailCommandHandler : IRequestHandler<UpdateAdminDetailCommandRequest, UpdateAdminDetailCommandResponse>
	{
		private readonly IImageRepository _imageRepository;
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public UpdateAdminDetailCommandHandler(IDetailRepository detailRepository, IImageRepository imageRepository, IMapper mapper)
		{
			_imageRepository = imageRepository;
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminDetailCommandResponse> Handle(UpdateAdminDetailCommandRequest request, CancellationToken ct = default)
		{
			var detailToUpdate = _mapper.Map<Detail>(request);

			if (request.ImageId != Guid.Empty)
			{
				var image = await _imageRepository.GetByIdAsync(request.ImageId, ct);
				detailToUpdate.ImageId = image.Id;
			}
			var updatedDetail = await _detailRepository.UpdateAsync(detailToUpdate, ct);
			var detailResponse = _mapper.Map<DetailResponse>(updatedDetail);

			return new UpdateAdminDetailCommandResponse(detailResponse);
		}
	}
}