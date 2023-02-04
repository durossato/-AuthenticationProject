using AuthenticationDuda.Models;

namespace AuthenticationDuda.Interfaces.Workers
{
    public interface IAuthenticationWorker
    {
        LoginResponse Login(LoginDto loginDto);
        bool Register(RegisterDto registerDto);
    }
}
