using Nexta.Domain.Abstractions.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Nexta.Domain.Options;
using System.Text;

namespace Nexta.Application.Services
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly JwtAuthOptions _options;
		public JwtTokenService(IOptions<JwtAuthOptions> options)
		{
			_options = options.Value;
		}

		public string CreateAccessToken(Guid id, string role)
		{
			var claims = new List<Claim> {
				new Claim("Id", id.ToString()),
				new Claim(ClaimTypes.Role, role),
				};

			var jwt = new JwtSecurityToken(
				issuer: _options.Issuer,
				audience: _options.Audience,
				claims: claims,
				expires: DateTime.UtcNow.Add(TimeSpan.FromDays(_options.ExpAccessTokenInDays)),
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(_options.Key)),
					SecurityAlgorithms.HmacSha256)
				);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public string CreateRefreshToken()
		{
			throw new NotImplementedException();
			/*
			var jwt = new JwtSecurityToken(
				issuer: _options.Issuer,
				audience: _options.Audience,
				expires: DateTime.UtcNow.Add(TimeSpan.FromDays(_options.ExpRefreshTokenInDays)),
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(_options.Key)),
					SecurityAlgorithms.HmacSha256)
				);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
			*/
		}
	}
}
