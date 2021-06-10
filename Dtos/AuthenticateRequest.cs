using System.ComponentModel.DataAnnotations;

namespace Food.Dtos
{
	public class AuthenticateRequest
	{
		[Required]
		[MaxLength(50)]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
