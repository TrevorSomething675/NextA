namespace Nexta.Infrastructure.Exceptions
{
    public class MinioFileCreationException : Exception
    {

		public MinioFileCreationException(string message)
			: base(message)
		{
		}
	}
}
