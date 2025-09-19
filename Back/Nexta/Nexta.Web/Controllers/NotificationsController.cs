using Nexta.Application.Queries.Notifications.GetNotificationsQuery;
using Nexta.Web.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NotificationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IResult> Get([FromRoute] GetNotificationsRequest request, CancellationToken ct = default)
        {
            var query = _mapper.Map<GetNotificationsQuery>(request);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }
    }
}