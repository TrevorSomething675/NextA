using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Domain.Models;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet(nameof(Test))]
		public async Task<IActionResult> Test()
        {
            var detail = new Detail
            {
                Name = "Name1",
                Article = "Article1",
                Description = "Description1"
            };

            return (IActionResult)detail;
        }

        public async Task<IActionResult> NoAuth()
        {
            return Ok("NoAuth");
		}

        [Authorize]
		public async Task<IActionResult> Auth()
		{
			return Ok("Auth");
		}
	}
}