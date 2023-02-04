using AuthenticationDuda.Models;

namespace AuthenticationDuda.Interfaces.Repositries
{
    public interface IUserRepository
    {
        void Insert(User user);
        List<User> Select(Func<User, bool> whereCondition);
    }
}
