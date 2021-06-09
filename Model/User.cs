using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Food.Model
{
	public class User : Entity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required]
		[MaxLength(50)]
		public string UserName { get; set; }

		[Required]
		[MaxLength(10)]
		public string Type { get; set; }

		public double longitude { get; set; }
		public double latitude { get; set; }

		public ICollection<Order> Orders { get; set; }

		public string RegistrationToken { get; set; }
		
		[JsonIgnore]
		[Required]
		public string Password { get; set; }
	}
}
