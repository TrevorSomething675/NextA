using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, GetNotificationsQueryResponse>
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(INotificationsRepository notificationsRepository, IMapper mapper)
        {
            _notificationsRepository = notificationsRepository;
            _mapper = mapper;
        }

        public async Task<GetNotificationsQueryResponse> Handle(GetNotificationsQuery query, CancellationToken ct)
        {
            var notifications = await _notificationsRepository.GetAsync(query.Filter, ct);
            var response = _mapper.Map<PagedData<NotificationResponse>>(notifications);

            return new GetNotificationsQueryResponse(response);
        }
    }
}