using AuthService.DbContexts;
using AuthService.Models;
using JwtTokenAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext _authContext;

        public AuthRepository(AuthContext authContext)
        {
            _authContext = authContext;
        }

        private readonly List<User> _users = new()
        {
        new("admin", "aDm1n", "leadership"),
        new("user01", "u$3r01", "teacher")
        };

        public AuthenticationToken GenerateAuthToken(Login user, string role)
        {
            User userLog;
            switch (role)
            {
                case "leadership":
                    userLog = _users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
                    break;
                case "teacher":
                    userLog = _users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
                    break;
                case "student":
                    userLog = _users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
                    break;
                default:
                    userLog = _users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
                    break;
            }
            
            if (user is null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(5);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, userLog.UserId),
            new Claim("Role", /*userLog.Role*/role)
        };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7128",
                claims: claims,
                expires: expirationTimeStamp,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var authenticationToken = new AuthenticationToken()
            {
                Token = tokenString,
                ExpiresIn = (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            };

            return authenticationToken;
        }
    }
    
}
