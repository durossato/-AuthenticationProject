using AuthenticationDuda.Models;

namespace AuthenticationDuda.System
{
    public static class FakeDataBase
    {
        public static List<User> Users { get; set; }= new List<User>();
    }
}
