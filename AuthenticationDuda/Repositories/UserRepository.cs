using AuthenticationDuda.Interfaces.Repositries;
using AuthenticationDuda.Models;
using AuthenticationDuda.System;

namespace AuthenticationDuda.Repositries
{
    public class UserRepository : IUserRepository
    {
        public void Insert(User user)
        {
            FakeDataBase.Users.Add(user);
        }

        public List<User> Select(Func<User, bool> whereCondition)
        {
            return FakeDataBase.Users.Where(whereCondition).ToList();
        }
    }
}
