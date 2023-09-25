﻿using FileService.DbContexts;
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
        public void ChangeFileName(string fileName, string fileId)
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

        public void DeleteFile(string fileId)
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

        public IEnumerable<PersonalFile> GetAllFile(string fileId)
        {
            return _dbContext.PersonalFiles.ToList();
        }

        public IEnumerable<PersonalFile> GetFileByName(string fileName)
        {
            return _dbContext.PersonalFiles.Where(e => _dbContext.FuzzySearch(e.FileName) == _dbContext.FuzzySearch(fileName)).ToList();
        }

        public IEnumerable<PersonalFile> GetFileBySubject(string subjectId)
        {
            return _dbContext.PersonalFiles.Where(e => e.SubjectId == subjectId);
        }

        public void WriteFile(IFormFile file, string fileName, string subJectId, string fileId)
        {
            var personalFile = new PersonalFile()
            {
                FileId = fileId,
                FileName = fileName,
                SubjectId = subJectId,
                DateTime = DateTime.Now,
                Size = 0,
                UserId = "admin"
            };
            _dbContext.Add(personalFile);
            Save();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
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
