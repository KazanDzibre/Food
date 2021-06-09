using Food.Model;

namespace Food.Core
{
	public interface IUserRepository : IRepository<User>
	{
		User GetUserById(int id);

		User GetUserWithUserAndPass(string UserName, string Password);

		User GetUserWithRegistratitonToken(string token);
	}
}
