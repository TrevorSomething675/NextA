namespace Nexta.Domain.Options
{
	public class DataBaseOptions
	{
		public const string Section = nameof(DataBaseOptions);
		public string ConnectionString { get; set; } = string.Empty;
	}
}