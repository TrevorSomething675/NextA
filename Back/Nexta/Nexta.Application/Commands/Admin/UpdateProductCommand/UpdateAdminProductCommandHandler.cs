using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
	public class UpdateAdminProductCommandHandler : IRequestHandler<UpdateAdminProductCommand, UpdateAdminProductCommandResponse>
	{
		private readonly IProductImageRepository _productImageRepository;
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public UpdateAdminProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductImageRepository productImageRepository)
		{
			_productImageRepository = productImageRepository;
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminProductCommandResponse> Handle(UpdateAdminProductCommand request, CancellationToken ct = default)
		{
			var productToUpdate = _mapper.Map<Product>(request);

			var updatedDbProduct = await _productRepository.UpdateAsync(productToUpdate, ct);

			if (productToUpdate.ImageId != null && productToUpdate.Image == null) // Удаление картинки
			{
				await _productImageRepository.RemoveAsync(productToUpdate.ImageId.Value);
			}
			else if (productToUpdate.ImageId != null && productToUpdate.Image != null) // Обновление картинки
			{
				var detailImage = productToUpdate.Image;
				detailImage.Id = productToUpdate.ImageId.Value;
				await _productImageRepository.UpdateAsync(productToUpdate.Image);
			}
			else if (productToUpdate.ImageId == null && productToUpdate.Image != null) //Добавление картинки
			{
				var detailImage = productToUpdate.Image;
				detailImage.ProductId = updatedDbProduct.Id;
				await _productImageRepository.AddAsync(detailImage); 
			}

			var result = _mapper.Map<ProductResponse>(updatedDbProduct);

			return new UpdateAdminProductCommandResponse(result);
		}
	}
}