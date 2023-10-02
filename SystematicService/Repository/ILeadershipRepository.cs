using SystematicService.Models;

namespace SystematicService.Repository
{
    public interface ILeadershipRepository
    {
        Task<SystemInfo> GetSystemInfo();
        Task AddSystemInfo(SystemInfo systemInfo);
        Task UpdateSystemInfo(SystemInfo systemInfo);
        Task<IEnumerable<Account>> GetUserByName(string userName);
        Task ChangeRole(string userId, string role);
        Task DeleteUser(string userId);
        Task AddUser(Account account);
        Task<IEnumerable<Account>> GetUsers();
        Task<IEnumerable<Account>> GetLeaderships();
        Task<IEnumerable<Account>> GetTeachers();
        Task<IEnumerable<Account>> GetStudents();
        Task AddStudentIntoClass(string userId, string classId);
    }
}
