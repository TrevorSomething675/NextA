using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

		//public async Task<IResult> Update()
        //{
        //
        //}
        //
		//public async Task<IResult> Add()
		//{
        //
		//}
	}
}