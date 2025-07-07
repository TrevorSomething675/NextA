using Nexta.Application.Commands.Admin.AddImageCommand;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	[ApiController]
	public class ImageController : Controller
    {
		private readonly IMediator _mediator;

		public ImageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(AddAdminImageCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Add([FromBody] AddAdminImageCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}