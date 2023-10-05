using FileService.DbContexts;
using FileService.Models;

namespace FileService.Repository
{
    public class LeadershipRepository : ILeadershipRepository
    {
        private readonly FileContext _dbContext;

        public LeadershipRepository(FileContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task ChangeFileName(string fileName, string fileId)
        {
            var file = new PersonalFile()
            {
                FileId = fileId,
                FileName = fileName
            };

            _dbContext.PersonalFiles.Attach(file);
            _dbContext.PersonalFiles.Entry(file).Property(x => x.FileName).IsModified = true;
            Save();
        }

        public async Task DeleteFile(string fileId)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileId);
            File.SetAttributes(filepath, FileAttributes.Normal);
            File.Delete(exactpath);
        }

        public IEnumerable<PersonalFile> GetAllFile(string userId)
        {
            return _dbContext.PersonalFiles.Where(e => e.UserId == userId).ToList();

        }

        public async Task<IEnumerable<PersonalFile>> GetFileByName(string userId, string fileName)
        {
            return _dbContext.PersonalFiles.Where(e => _dbContext.FuzzySearch(e.FileName) == _dbContext.FuzzySearch(fileName)&& e.UserId==userId).ToList();
        }

        public async Task<IEnumerable<PersonalFile>> GetFileBySubject(string userId, string subjectId)
        {
            return _dbContext.PersonalFiles.Where(e => e.SubjectId == subjectId && e.UserId==userId);
        }

        public async Task WriteFile(IFormFile file,string userId, string fileName, string subJectId, string fileId)
        {
            var personalFile = new PersonalFile();
            personalFile.FileId = fileId;
            personalFile.FileName = fileName;
            personalFile.SubjectId = subJectId;
            personalFile.DateTime = DateTime.Now;
            personalFile.Size = file.Length;
            personalFile.UserId = userId;   
            _dbContext.Add(personalFile);
            Save();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                
                filename = personalFile.FileId + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
