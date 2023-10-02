using AccountService.Models;

namespace AccountService.Repository
{
    public interface IAccountRepository
    {
        Account GetAccount(string userId);

        Task<string> WriteFile(IFormFile file, string userId);
        Task DeleteAvatar(string fileName);
        string ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
