using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Food.Configuration;
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

		public UserController(IMapper mapper, ProjectConfiguration configuration) : base(configuration)
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

		[HttpGet("{id}")]
		public ActionResult<User> GetUserById(int id)
		{
			var userModel = _userService.GetById(id);
			if (userModel != null)
			{
				return Ok(userModel);
			}
			return NotFound();
		}

		[HttpGet]
		public ActionResult<IEnumerable<User>> GetAll()
		{
			var users = _userService.GetAll();
			if(users != null)
			{
				return Ok(users);
			}
			return NotFound();
		}

	}
}
