using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Specification;
using Nexta.Application.DTO.Users;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PagedData<NotificationDto>>
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(INotificationsRepository notificationsRepository, IMapper mapper)
        {
            _notificationsRepository = notificationsRepository;
            _mapper = mapper;
        }

        public async Task<PagedData<NotificationDto>> Handle(GetNotificationsQuery query, CancellationToken ct)
        {
            var spec = new NotificationSpecification(query.SearchTerm, query.UserId, query.PageNumber, query.PageSize);

            var notifications = await _notificationsRepository.GetAsync(spec, ct);
            var response = _mapper.Map<PagedData<NotificationDto>>(notifications);

            return response;
        }
    }
}