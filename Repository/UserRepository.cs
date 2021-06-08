using System.Linq;
using Food.Core;
using Food.Model;

namespace Food.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{

		public UserRepository(Context context) : base(context)
		{

		}

		public User GetUserById(int id)
		{
			return context.Users.Where(x => x.Id == id).FirstOrDefault();
		}
	}
}
