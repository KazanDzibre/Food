using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Food.Dtos
{
	public class UserRegisterDto
	{
		public string FirstName { get; set; }
		public string LastName {get; set;}
		[Required]
		[MaxLength(50)]
		public string UserName { get; set; }
	
		[Required]
		[MaxLength(10)]
		public string Type { get; set; } 

		public double longitude { get; set; }
		public double latitude { get; set; }
		
		[Required]
		public string Password { get; set; }
	}
}
