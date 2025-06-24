using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Nexta.Domain.Options;
using System.Text;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionAuthExtension
	{
		public static void AddAppAuth(this IServiceCollection services, IConfiguration configuration)
		{
			var authOptions = configuration.GetSection(JwtAuthOptions.Section).Get<JwtAuthOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = authOptions.Issuer,
					ValidateAudience = true,
					ValidAudience = authOptions.Audience,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
					ValidateIssuerSigningKey = true,
				};
			});
			services.AddAuthorization();
		}

		public static void UseAppAuth(this IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}