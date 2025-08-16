using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.CreateAdminDetailCommand
{
    public class CreateAdminDetailCommandHandler : IRequestHandler<CreateAdminDetailCommandRequest, CreateAdminDetailCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDetailRepository _detailRepository;

        public CreateAdminDetailCommandHandler(IDetailRepository detailRepository, IMapper mapper)
        {
            _detailRepository = detailRepository;
            _mapper = mapper;
        }

        public async Task<CreateAdminDetailCommandResponse> Handle(CreateAdminDetailCommandRequest request, CancellationToken ct = default)
        {
            var detail = _mapper.Map<Detail>(request);
            var createdDetailId = await _detailRepository.CreateAsync(detail, ct);

            return new CreateAdminDetailCommandResponse(createdDetailId);
        }
    }
}