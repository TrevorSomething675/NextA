namespace Nexta.Domain.Options
{
    public class MinioOptions
    {
		public const string Section = nameof(MinioOptions);
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
		public bool UseSSL { get; set; }
		public string Endpoint { get; set; }
	}
}