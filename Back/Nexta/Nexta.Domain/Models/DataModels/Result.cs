using Microsoft.AspNetCore.Mvc;

namespace Nexta.Domain.Models.DataModels
{
	public class Result<T>
	{
		public T? Value { get; set; }
		public int StatusCode { get; set; } = 200;
		public List<string> ErrorMessages { get; set; } = new List<string>();

		public Result() { }
		public Result(T? value)
		{
			Value = value;
		}

		public Result<T> Success()
		{
			StatusCode = 200;
			return this;
		}
		public Result<T> NotFound(string message = "")
		{
			StatusCode = 404;
			return this;
		}
		public Result<T> BadRequest(string message = "")
		{
			ErrorMessages.Add(message);
			StatusCode = 400;
			return this;
		}
		public Result<T> Invalid(string message = "")
		{
			ErrorMessages.Add(message);
			StatusCode = 400;
			return this;

		}

		public ActionResult ToActionResult()
		{
			switch (StatusCode)
			{
				case 200:
					return new OkObjectResult(this);
				case 404:
					return new NotFoundObjectResult(this);
				case 400:
					return new BadRequestObjectResult(this);
				default:
					return new ObjectResult(this) { StatusCode = StatusCode };
			}
		}
	}
}
