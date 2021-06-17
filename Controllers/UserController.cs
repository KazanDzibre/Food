using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Food.Core;
using Food.Dtos;
using Food.Model;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase 
	{
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

		public UserController(IMapper mapper, IUserService userService)
		{
            _userService = userService;
			_mapper = mapper;
		}


		//POST /user
		
		[HttpPost]   //c
		[Authorize("Admin")]
		public ActionResult<User> Register(UserRegisterDto userRegisterDto)
		{
			var userModel = _mapper.Map<User>(userRegisterDto);

			User user = _userService.Add(userModel);

			return Ok(user);
		}

		[HttpPost("authenticate")]
		public IActionResult Authenticate(AuthenticateRequest model)
		{
			var response = _userService.Authenticate(model);

			if(response == null)
			{
				return BadRequest(new { message = "Wrong Username or Password!" });
			}
			return Ok(response);
		}

		[HttpGet("{id}")] //R
		public ActionResult<User> GetUserById(int id)
		{
			var userModel = _userService.GetUserById(id);
			if (userModel != null)
			{
				return Ok(userModel);
			}
			return NotFound();
		}

		[HttpGet]  //R A
		public ActionResult<IEnumerable<User>> GetAll()
		{
			var users = _userService.GetAll();
			if(users != null)
			{
				return Ok(users);
			}
			return NotFound();
		}

		[Route("/api/user/create")]
		[HttpPost] //C
		public async Task<IActionResult> CreateUser(UserRegisterDto userData)
		{

			User user = _userService.GetUserWithUserAndPass(userData.UserName,userData.Password);
			if(user != null)
			{
				return BadRequest("User exists");
			}


			var userModel = _mapper.Map<User>(userData);

			Boolean success = _userService.CreateUserAndSendToken(userModel);

			if(success)
			{
				return Ok(userModel);
			}
			else
			{
				return BadRequest("Something went wrong");
			}
		}
		[HttpDelete("{id}")] //D
		public void DeleteUser(int id)
		{
			User user = _userService.GetUserById(id);
			_userService.Remove(user);
		}
	}
}
