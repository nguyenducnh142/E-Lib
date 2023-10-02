using Microsoft.EntityFrameworkCore;
using System.Data;
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
        private void Save()
        {
            _dbContext.SaveChanges();
        }


        public async Task AddSystemInfo(SystemInfo systemInfo)
        {
            _dbContext.Add(systemInfo);
            Save();
        }

        public async Task ChangeRole(string userId, string role)
        {
            _dbContext.Accounts.Find(userId).Role = role;
            Save();
        }

        public async Task DeleteUser(string userId)
        {
            _dbContext.Remove(_dbContext.Accounts.Find(userId));
            Save();
        }


        public async Task<SystemInfo> GetSystemInfo()
        {
            return _dbContext.SystemInfos.Find("system");
        }

        public async Task<IEnumerable<Account>> GetUserByName(string userName)
        {
            return _dbContext.Accounts.Where(e => (
            _dbContext.FuzzySearch(e.UserName) == _dbContext.FuzzySearch(userName)))
                .ToList();
        }

        public async Task<IEnumerable<Account>> GetUsers()
        {
            return _dbContext.Accounts.ToList();
        }

        public async Task<IEnumerable<Account>> GetLeaderships()
        {
            return _dbContext.Accounts.Where(e=> e.Role=="leadership").ToList();
        }
        public async Task<IEnumerable<Account>> GetTeachers()
        {
            return _dbContext.Accounts.Where(e => e.Role == "teacher").ToList();
        }
        public async Task<IEnumerable<Account>> GetStudents()
        {
            return _dbContext.Accounts.Where(e => e.Role == "student").ToList();
        }

        public async Task UpdateSystemInfo(SystemInfo systemInfo)
        {
            _dbContext.Entry(systemInfo).State = EntityState.Modified;
            Save();
        }

        public async Task AddUser(Account account)
        {
            _dbContext.Add(account);
            Save();
        }

        public async Task AddStudentIntoClass(string userId, string classId)
        {
            var tmp = _dbContext.StudentClasses.Where(e => e.UserId == userId && e.ClassId==classId).FirstOrDefault();
            if (tmp == null)
            {
                var studentClass = new StudentClass();
                studentClass.UserId = userId;
                studentClass.ClassId = classId;
                _dbContext.Add(studentClass);
                Save();
            }

        }
    }
}
