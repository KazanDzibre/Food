using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public User GetUserWithRegistratitonToken(string token)
        {
			return context.Users.Where(x => (x.RegistrationToken == token)).FirstOrDefault();
        }

        public User GetUserWithUserAndPass(string UserName, string Password)
        {
			return context.Users.Where(x => (x.UserName == UserName && x.Password == Password)).FirstOrDefault();
        }

		public IEnumerable<User> GetUsersByType(string type)
		{
			return context.Users.Where(x => ( x.Type == type )).ToList();
		}
    }
}
