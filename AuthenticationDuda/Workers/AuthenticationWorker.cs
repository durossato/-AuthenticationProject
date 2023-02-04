using AuthenticationDuda.Helpers;
using AuthenticationDuda.Interfaces.Repositries;
using AuthenticationDuda.Interfaces.Workers;
using AuthenticationDuda.Models;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationDuda.Workers
{
    public class AuthenticationWorker : IAuthenticationWorker
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtManager _jwtManager;
        private byte[] passwordSalt = Array.Empty<byte>();
        private byte[] passwordHash = Array.Empty<byte>();

        public AuthenticationWorker(IUserRepository userRepository, JwtManager jwtManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
        }

        public LoginResponse Login(LoginDto loginDto)
        {
            var user = _userRepository.Select(u => u.Email == loginDto.Email)[0];

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var hashedLoginDtoPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            if (!user.PasswordHash.SequenceEqual(hashedLoginDtoPassword))
                return new() { Success = false };
            else
                return new() {
                    Success = true,
                    Token = _jwtManager.GenerateToken(loginDto)
                };
        }

        public bool Register(RegisterDto registerDto)
        {
            
            //validou usuário ok
            try
            {
                EncryptPassword(registerDto.Password);

                var user = new User()
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                _userRepository.Insert(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void EncryptPassword(string password)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
