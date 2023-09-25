using FileService.Models;

namespace FileService.Repository
{
    public interface ILeadershipRepository
    {
        void ChangeFileName(string fileName, string fileId);
        void DeleteFile(string fileId);
        IEnumerable<PersonalFile> GetAllFile(string fileId);
        IEnumerable<PersonalFile> GetFileByName(string fileName);
        IEnumerable<PersonalFile> GetFileBySubject(string subjectId);
        void WriteFile(IFormFile file, string fileName, string subjectId, string fileId);
    }
}
