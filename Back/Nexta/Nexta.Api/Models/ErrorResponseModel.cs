namespace Nexta.Web.Models
{
	public class ErrorResponseModel
	{
		public string Message { get; set; }
		public int StatusCode { get; set; }

		public ErrorResponseModel(string message, int statusCode)
		{
			Message = message;
			StatusCode = statusCode;
		}
	}
}