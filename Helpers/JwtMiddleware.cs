using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Food.Helpers
{
	public class JwtMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public UserService _userService = new UserService();

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
		{
			_next = next;
			_appSettings = appSettings.Value;
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			if(token != null)
			{
				attachUserToContext(context, token);
			}
			await _next(context);
		}

		public void attachUserToContext(HttpContext context, string token)
		{
			try
			{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken validatedToken);

			var jwtToken = (JwtSecurityToken)validatedToken;
			var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
			context.Items["User"] = _userService.GetById(userId);
			}
			catch(Exception e)
			{
				//nothing
			}
		}
	}
}
