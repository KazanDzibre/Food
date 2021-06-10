using System.ComponentModel.DataAnnotations;

namespace Food.Dtos
{
	public class OrderPatchDto
	{
		[Required]
		public int DriverId { get; set; }
	}
}
