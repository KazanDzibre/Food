using System;
using System.Collections.Generic;
using Food.Configuration;
using Food.Model;
using Food.Repository;

namespace Food.Service
{
	public class UserService
	{
		public UserService(){}

		public UserService(ProjectConfiguration configuration)
		{

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
	}
}
