using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using Nexta.Application.Enums;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateAdminProductCommand
{
	public class UpdateAdminProductCommandHandler : IRequestHandler<UpdateAdminProductCommand, UpdateAdminProductCommandResponse>
	{
		private readonly IProductImageRepository _productImageRepository;
		private readonly IProductsRepository _productsRepository;
		private readonly IMapper _mapper;

		public UpdateAdminProductCommandHandler(IProductsRepository productRepository, IMapper mapper, IProductImageRepository productImageRepository)
		{
			_productImageRepository = productImageRepository;
			_productsRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminProductCommandResponse> Handle(UpdateAdminProductCommand command, CancellationToken ct = default)
		{
			var productToUpdate = _mapper.Map<Product>(command);

			var updatedDbProduct = await _productsRepository.UpdateAsync(productToUpdate, ct);

			switch (command.Type)
			{	/*
				case ProductOperationType.Update:
					productToUpdate.Image.ProductId = productToUpdate.Image.ProductId;
					await _productImageRepository.UpdateAsync(productToUpdate.Image, ct);
					break;
				*/
				case ProductOperationType.Create:
                    productToUpdate.Image.ProductId = updatedDbProduct.Id;
					await _productImageRepository.AddAsync(productToUpdate.Image, ct);
					break;
				case ProductOperationType.Delete:
					await _productImageRepository.DeleteAsync(updatedDbProduct.ImageId.Value, ct);
					break;

				default:
					break;
			}

			var result = _mapper.Map<AdminProductResponse>(updatedDbProduct);

			return new UpdateAdminProductCommandResponse(result);
		}
	}
}