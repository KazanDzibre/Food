using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Food.Dtos;
using Food.Model;

namespace Food.Core
{
    public interface IUserService
    {
        User Add(User user);        

        User GetUserById(int id);

        IEnumerable<User> GetAll();

        void Remove(User user);

        User GetUserWithRegistrationToken(string token);

        User GetUserWithUserAndPass(string UserName, string Password);

        Boolean CreateUserAndSendToken(User user);

        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
