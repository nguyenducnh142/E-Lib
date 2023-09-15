using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
using SubjectService.Migrations;
using SubjectService.Models;

namespace SubjectService.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SubjectContext _dbContext;

        public SubjectRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = _dbContext.Subjects.Find(subjectId);
            _dbContext.Subjects.Remove(subject);
            Save();
        }


        public Subject GetSubjectByID(int subjectId)
        {
            return _dbContext.Subjects.Find(subjectId);
        }

        public IEnumerable<Subject> GetSubjectByName(string subjectName)
        {
            return _dbContext.Subjects.Where(e=>_dbContext.FuzzySearch(e.SubjectName) == _dbContext.FuzzySearch(subjectName)).ToList();
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return _dbContext.Subjects.ToList();
        }

        public void InsertLesson(Lesson lesson)
        {
            _dbContext.Add(lesson);
            Save();
        }


        public void InsertSubject(Subject subject)
        {
            _dbContext.Add(subject);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateSubject(Subject subject)
        {
            _dbContext.Entry(subject).State=EntityState.Modified;
            Save();
        }

        public string WriteFile(IFormFile file, string lessonFileName, int lessonId, string lessonFileDescription)
        {
            LessonFile lessonFile = new LessonFile();
            lessonFile.LessonId= lessonId;
            lessonFile.LessonFileName= lessonFileName;
            lessonFile.LessonFileDescription= lessonFileDescription;
            _dbContext.Add(lessonFile);
            Save();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
                filename = lessonFile.LessonFileName + extension;

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
            return filename;
        }

        public void InsertQuestion(Question question)
        {
            _dbContext.Add(question);
            Save();
        }

        public void InsertSubjectNoti(SubjectNotification subjectNotification)
        {
            _dbContext.Add(subjectNotification);
            Save();
        }
    }
}
