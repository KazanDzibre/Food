using Food.Configuration;
using Food.Service;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DefaultController : ControllerBase
	{
		protected UserService _userService;
		protected OrderService _orderService;
		protected ProjectConfiguration _configuration;

		public DefaultController()
		{
			this._userService = new UserService();
			this._orderService = new OrderService();
		}
	}
}
