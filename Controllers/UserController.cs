using System.Threading.Tasks;
using AutoMapper;
using Food.Dtos;
using Food.Model;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : DefaultController
	{
		private readonly IMapper _mapper;

		public UserController(IMapper mapper)
		{
			_mapper = mapper;
		}

		//POST /user
		
		[HttpPost]
		public ActionResult<User> Register(UserRegisterDto userRegisterDto)
		{
			var userModel = _mapper.Map<User>(userRegisterDto);

			User user = _userService.Add(userModel);

			return Ok(user);
		}

	}
}
