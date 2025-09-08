using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Admin;
using Nexta.Application.Enums;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Admin.UpdateProductCommand
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

		public async Task<UpdateAdminProductCommandResponse> Handle(UpdateAdminProductCommand command, CancellationToken ct = default)
		{
			var productToUpdate = _mapper.Map<Product>(command);

			var updatedDbProduct = await _productRepository.UpdateAsync(productToUpdate, ct);

			switch (command.Type)
			{
				case PrdouctOperationType.Update:
					await _productImageRepository.UpdateAsync(productToUpdate.Image, ct);
					break;
				case PrdouctOperationType.Create:
					await _productImageRepository.AddAsync(productToUpdate.Image, ct);
					break;
				case PrdouctOperationType.Delete:
					await _productImageRepository.DeleteAsync(productToUpdate.ImageId.Value, ct);
					break;

				default:
					break;
			}

			var result = _mapper.Map<AdminProductResponse>(updatedDbProduct);

			return new UpdateAdminProductCommandResponse(result);
		}
	}
}