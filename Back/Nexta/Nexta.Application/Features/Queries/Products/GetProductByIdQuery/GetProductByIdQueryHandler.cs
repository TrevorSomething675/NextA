using Nexta.Application.DTO.Products;
using AutoMapper;
using MediatR;
using Nexta.Application.Abstractions;

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
            var product = await _unitOfWork.Products.GetByIdAsync(query.Id, ct);
            var response = _mapper.Map<ProductDto>(product);

            return response;
        }
    }
}