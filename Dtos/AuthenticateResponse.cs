using Food.Model;

namespace Food.Dtos
{
	public class AuthenticateResponse
	{
		public string UserName { get; set; }
		public string Type { get; set; }
		public string Token { get; set; }

		public AuthenticateResponse(User user, string token)
		{
			UserName = user.UserName;
			Type = user.Type;
			Token = token;
		}
	}
}
