using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Food.Configuration;
using Food.Dtos;
using Food.Helpers;
using Food.Model;
using Food.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Food.Service
{
	public class UserService
	{
        private readonly AppSettings _appSettings;

        public UserService(){}

		public UserService(ProjectConfiguration configuration)
		{

		}

		public UserService(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}
		public User Add(User user)
		{
			if(user == null)
			{
				return null;
			}
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					unitOfWork.Users.Add(user);
					unitOfWork.Complete();
				}
			}
			catch (Exception e)
			{
				return null;
			}
			return user;
		}

		public User GetById(int id)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					var user = unitOfWork.Users.GetUserById(id);
					if(user == null)
					{
						return null;
					}
					return user;
				}
			}
			catch(Exception e)
			{
				return null;
			}
		}
		
		public IEnumerable<User> GetAll()
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					var users = unitOfWork.Users.GetAll();
					if(users == null)
					{
						return null;
					}
					return users;
				}
			}
			catch(Exception e)
			{
				return null;
			}

		}

		public void Remove(User user)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					unitOfWork.Users.Remove(user);
					unitOfWork.Complete();
				}
			}
			catch( Exception e )
			{
				//nothing
			}
		}

		public User GetUserWithRegistrationToken(string token)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					return unitOfWork.Users.GetUserWithRegistratitonToken(token);
				}
			}
			catch (Exception e)
			{
				return null;
			}
		}
		public User GetUserWithUserAndPass(string UserName, string Password)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					return unitOfWork.Users.GetUserWithUserAndPass(UserName,Password);
				}
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public Boolean CreateUserAndSendToken(User user)
		{
			if(user == null)
			{
				return false;
			}
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					user.RegistrationToken = Guid.NewGuid().ToString();
					unitOfWork.Users.Add(user);
					unitOfWork.Complete();
				}
			}
			catch(Exception e)
			{
				return false;
			}
			return true;
		}

		public AuthenticateResponse Authenticate(AuthenticateRequest model)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					var user = unitOfWork.Users.GetUserWithUserAndPass(model.UserName, model.Password);

					if(user == null) return null;

					var token = generateJwtToken(user);

					return new AuthenticateResponse(user, token);
				}
			}
			catch(Exception e)
			{
				return null;
			}
		}

		private string generateJwtToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("sadaspoasdijasdijasdoijasogijasdoj");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new [] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
