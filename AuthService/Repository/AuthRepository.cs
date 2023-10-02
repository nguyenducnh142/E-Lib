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



        public AuthenticationToken GenerateAuthToken(Login user)
        {
            var userLog = _authContext.Accounts.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);

            if (userLog is null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(5);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, userLog.UserId),
            new Claim("Role", userLog.Role)
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
