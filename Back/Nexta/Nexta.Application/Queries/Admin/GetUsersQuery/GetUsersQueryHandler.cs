using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedData<AdminUserResponse>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<PagedData<AdminUserResponse>> Handle(GetUsersQuery query, CancellationToken ct)
        {
            var users = await _usersRepository.GetAllAsync(query.Filter, ct);

            var response = _mapper.Map<PagedData<AdminUserResponse>>(users);

            return response;
        }
    }
}