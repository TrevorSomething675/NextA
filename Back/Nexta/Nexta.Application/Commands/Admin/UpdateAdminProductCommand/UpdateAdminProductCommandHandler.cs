using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateAdminProductCommand
{
	public class UpdateAdminProductCommandHandler : IRequestHandler<UpdateAdminProductCommand, UpdateAdminProductCommandResponse>
	{
		private readonly IMapper _mapper;

		public UpdateAdminProductCommandHandler(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<UpdateAdminProductCommandResponse> Handle(UpdateAdminProductCommand command, CancellationToken ct = default)
		{
			throw new Exception("UpdateAdminProductCommandHandler");
			/*
			var productToUpdate = _mapper.Map<Product>(command);

			var updatedDbProduct = await _productsRepository.UpdateAsync(productToUpdate, ct);

			switch (command.Type)
			{	
				case ProductOperationType.Update:
					productToUpdate.Image.ProductId = productToUpdate.Image.ProductId;
					await _productImageRepository.UpdateAsync(productToUpdate.Image, ct);
					break;
				case ProductOperationType.Create:
                    productToUpdate.Id = updatedDbProduct.Id;
					await _productImageRepository.AddAsync(productToUpdate, ct);
					break;
				case ProductOperationType.Delete:
					await _productImageRepository.DeleteAsync(updatedDbProduct.ImageId.Value, ct);
					break;

				default:
					break;
			}

			var result = _mapper.Map<AdminProductResponse>(updatedDbProduct);

			return new UpdateAdminProductCommandResponse(result);
			*/
		}
	}
}