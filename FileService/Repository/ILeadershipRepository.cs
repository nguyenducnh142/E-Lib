using FileService.Models;

namespace FileService.Repository
{
    public interface ILeadershipRepository
    {
        Task ChangeFileName(string fileName, string fileId);
        Task DeleteFile(string fileId);
        Task<IEnumerable<PersonalFile>> GetAllFile(string userId);
        Task<IEnumerable<PersonalFile>> GetFileByName(string userId, string fileName);
        Task<IEnumerable<PersonalFile>> GetFileBySubject(string userId, string subjectId);
        Task WriteFile(IFormFile file, string userId, string fileName, string subjectId, string fileId);
    }
}
