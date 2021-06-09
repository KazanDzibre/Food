using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Food.Configuration;
using Food.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Food.Controllers
{
	public class TokenController : DefaultController
	{
		public TokenController(ProjectConfiguration configuration) : base(configuration){  }

		[HttpPost]
		public async Task<IActionResult> Post(User userData)
		{
			if(userData == null)
			{
				return BadRequest();
			}

			User user = _userService.GetUserWithUserAndPass(userData.UserName, userData.Password);

			if(user == null)
			{
				return BadRequest("Invalid credentials");
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, _configuration.Jwt.Subject),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				new Claim("Id", user.Id.ToString()),
				new Claim("FirstName", user.FirstName),
				new Claim("LastName", user.LastName),
				new Claim("UserName", user.UserName)
			};
			
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.Key));
			
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(_configuration.Jwt.Issuer, _configuration.Jwt.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

			return Ok(new JwtSecurityTokenHandler().WriteToken(token));
		}
	}
}
