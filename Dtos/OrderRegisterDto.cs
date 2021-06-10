using System.ComponentModel.DataAnnotations;

namespace Food.Dtos
{
	public class OrderRegisterDto
	{
		[Required]
		public string Restaurant { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string FoodChoice { get; set; }
	}
}
