using AuthenticationDuda.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationDuda.Helpers
{
    public class JwtManager
    {
        private readonly IConfiguration _config;

        public JwtManager(IConfiguration config)
        {
            _config = config;
        }

        internal string GenerateToken(LoginDto loginDto)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, loginDto.Email)
            };

            var jwtKey = _config.GetSection("JwtSettings:SecurityKey").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenExpiringTime = Double.Parse(_config.GetSection("JwtSettings:TokenExpiringTimeInHours").Value);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(tokenExpiringTime),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
