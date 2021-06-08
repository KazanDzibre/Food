using System.ComponentModel.DataAnnotations;

namespace Food.Model
{
	public class Order : Entity
	{
		[Required]
		public string Restaurant { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string FoodChoice { get; set; }

		public User User { get; set; }

		public int? DriverId { get; set; }
	}
}
