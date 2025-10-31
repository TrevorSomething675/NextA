using Nexta.Application.DTO.Admin;
using Nexta.Domain.Specification;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedData<AdminUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedData<AdminUserResponse>> Handle(GetUsersQuery query, CancellationToken ct)
        {
            var spec = new AdminUsersSpecification(query.SearchTerm, query.PageNumber, query.PageSize);
            var users = await _unitOfWork.Users.GetAsync(spec, ct);

            var response = _mapper.Map<PagedData<AdminUserResponse>>(users);

            return response;
        }
    }
}