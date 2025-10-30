using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Categories;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Categories.GetCategoriesQuery
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken ct)
        {
            var categories = await _categoriesRepository.GetAsync(ct);

            var response = _mapper.Map<List<CategoryDto>>(categories);

            return response;
        }
    }
}