using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.Products;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Categories.AddCategoryCommand
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IValidator<AddCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(ICategoriesRepository categoriesRepository,
            IValidator<AddCategoryCommand> validator, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddCategoryCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var category = _mapper.Map<Category>(command);
            var createdCategoryId = await _categoriesRepository.AddAsync(category, ct);

            return createdCategoryId;
        }
    }
}