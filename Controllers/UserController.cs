using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Food.Configuration;
using Food.Dtos;
using Food.Model;
using Microsoft.AspNetCore.Authorization;
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
		
		[Authorize]
		[HttpPost]   //c
		public ActionResult<User> Register(UserRegisterDto userRegisterDto)
		{
			var userModel = _mapper.Map<User>(userRegisterDto);

			User user = _userService.Add(userModel);

			return Ok(user);
		}

		[HttpGet("{id}")] //R
		public ActionResult<User> GetUserById(int id)
		{
			var userModel = _userService.GetById(id);
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
			User user = _userService.GetById(id);
			_userService.Remove(user);
		}
	}
}
