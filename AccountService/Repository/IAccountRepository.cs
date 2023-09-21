using AccountService.Models;

namespace AccountService.Repository
{
    public interface IAccountRepository
    {
        Account GetAccountDetail(string accountId);
        void InsertAccount (Account account);

        string WriteFile(IFormFile file, string userId);
        void DeleteAvatar(string fileName);
        string ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
