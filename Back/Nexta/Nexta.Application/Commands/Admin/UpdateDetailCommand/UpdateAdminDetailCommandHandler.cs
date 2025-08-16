using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
	public class UpdateAdminDetailCommandHandler : IRequestHandler<UpdateAdminDetailCommandRequest, UpdateAdminDetailCommandResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IDetailImageRepository _detailImageRepository;
		private readonly IMapper _mapper;

		public UpdateAdminDetailCommandHandler(IDetailRepository detailRepository, IMapper mapper, IDetailImageRepository detailImageRepository)
		{
			_detailImageRepository = detailImageRepository;
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminDetailCommandResponse> Handle(UpdateAdminDetailCommandRequest request, CancellationToken ct = default)
		{
			var detailToUpdate = _mapper.Map<Detail>(request);

			var updatedDbDetail = await _detailRepository.UpdateAsync(detailToUpdate, ct);

			if (detailToUpdate.ImageId != null && detailToUpdate.Image == null) // Удаление картинки
			{
				await _detailImageRepository.RemoveAsync(detailToUpdate.ImageId.Value);
			}
			else if (detailToUpdate.ImageId != null && detailToUpdate.Image != null) // Обновление картинки
			{
				var detailImage = detailToUpdate.Image;
				detailImage.Id = detailToUpdate.ImageId.Value;
				await _detailImageRepository.UpdateAsync(detailToUpdate.Image);
			}
			else if (detailToUpdate.ImageId == null && detailToUpdate.Image != null) //Добавление картинки
			{
				var detailImage = detailToUpdate.Image;
				detailImage.DetailId = updatedDbDetail.Id;
				await _detailImageRepository.AddAsync(detailImage); 
			}

			var detailResponse = _mapper.Map<DetailResponse>(updatedDbDetail);
			return new UpdateAdminDetailCommandResponse(detailResponse);
		}
	}
}