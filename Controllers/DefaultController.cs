using Food.Service;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DefaultController : ControllerBase
	{
		protected UserService _userService;
		// protected OrderService _orderService;

		public DefaultController()
		{
			this._userService = new UserService();
		}
	}
}
