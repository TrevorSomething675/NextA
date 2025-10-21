using Nexta.Application.DTO.Product;
using Nexta.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct = default)
        {
            var product = await _unitOfWork.Products.GetAsync(query.Id, ct);
            var response = _mapper.Map<ProductDto>(product);

            return response;
        }
    }
}