namespace Nexta.Domain.Options
{
    public class JwtAuthOptions
    {

		public const string Section = nameof(JwtAuthOptions);

		public string Issuer { get; set; } = null!;
		public string Audience { get; set; } = null!;
		public string Key { get; set; } = null!;
		public int ExpAccessTokenInDays { get; set; }
		public int ExpRefreshTokenInDays { get; set; }
	}
}
