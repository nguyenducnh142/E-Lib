using SystematicService.Models;

namespace SystematicService.Repository
{
    public interface ILeadershipRepository
    {
        IEnumerable<Account> GetAllAccount();
        SystemInfo GetSystemInfo();
        void AddSystemInfo(SystemInfo systemInfo);
        void UpdateSystemInfo(SystemInfo systemInfo);
        IEnumerable<Account> GetUserByName(string userName);
        void AddAccount(Account account);
        void ChangeRole(string userId, int role);
        void DeleteUser(string userId);
        IEnumerable<Account> GetUserByRole(int role);
    }
}
