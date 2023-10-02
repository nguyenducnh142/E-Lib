using AuthService.Models;

namespace AuthService.Repository
{
    public interface IAuthRepository
    {
        AuthenticationToken GenerateAuthToken(Login user);
    }
}
