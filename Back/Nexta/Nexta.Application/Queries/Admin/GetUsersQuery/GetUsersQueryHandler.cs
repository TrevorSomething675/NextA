using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery query, CancellationToken ct)
        {
            var users = await _usersRepository.GetAllAsync(query.Filter, ct);

            var usersResponse = _mapper.Map<PagedData<AdminUserResponse>>(users);

            return new GetUsersQueryResponse(usersResponse);
        }
    }
}