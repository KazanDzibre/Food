using Food.Model;

namespace Food.Core
{
	public interface IUserRepository : IRepository<User>
	{
		User GetUserById(int id);
	}
}
