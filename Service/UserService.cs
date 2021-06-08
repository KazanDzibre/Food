using System;
using Food.Model;
using Food.Repository;

namespace Food.Service
{
	public class UserService
	{
		public UserService(){}

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
	}
}
