using Microsoft.EntityFrameworkCore;
using SystematicService.DbContexts;
using SystematicService.Models;

namespace SystematicService.Repository
{
    public class LeadershipRepository : ILeadershipRepository
    {
        private readonly SystemContext _dbContext;

        public LeadershipRepository(SystemContext context)
        {
            _dbContext = context;
        }

        public void AddAccount(Account account)
        {
            _dbContext.Add(account);
            _dbContext.SaveChanges();
        }

        public void AddSystemInfo(SystemInfo systemInfo)
        {
            _dbContext.Add(systemInfo);
            _dbContext.SaveChanges();
        }

        public void ChangeRole(string userId, int role)
        {
            var account = _dbContext.Accounts.Find(userId);
            account.Role= role;
            _dbContext.SaveChanges();
        }

        public void DeleteUser(string userId)
        {
            var account = _dbContext.Accounts.Find(userId);
            _dbContext.Remove(account);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccount()
        {
            return _dbContext.Accounts.ToList();
        }

        public SystemInfo GetSystemInfo()
        {
            return _dbContext.SystemInfos.Find("system");
        }

        public IEnumerable<Account> GetUserByName(string userName)
        {
            return _dbContext.Accounts.Where(e => (
            _dbContext.FuzzySearch(e.UserName) == _dbContext.FuzzySearch(userName)))
                .ToList();
        }

        public IEnumerable<Account> GetUserByRole(int role)
        {
            return _dbContext.Accounts.Where(e => e.Role==role).ToList();
        }

        public void UpdateSystemInfo(SystemInfo systemInfo)
        {
            _dbContext.Entry(systemInfo).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


    }
}
