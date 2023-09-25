using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SubjectService.DBContexts;
//using SubjectService.Migrations;
using SubjectService.Models;
using System.Collections.Generic;

namespace SubjectService.Repository
{
    public class LeadershipRepository : ILeadershipRepository
    {
        private readonly SubjectContext _dbContext;

        public LeadershipRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<Subject> GetSubjects()
        {
            //GetSubjectNotAproved
            List<string> lessonIds = _dbContext.LessonsFiles.Where(e => e.Approve == false).Select(e => e.LessonId).ToList();
            List<string> subjectIds = new List<string>();
            List<Subject> subjects = new List<Subject>();
            foreach (var lessonId in lessonIds)
            {
                string subjectIdsTmp = _dbContext.Lessons.Where(e => e.LessonId == lessonId).Select(e => e.SubjectId).FirstOrDefault();
                subjectIds.Add(subjectIdsTmp);
            }
            List<string> distinctSubjectIds = subjectIds.Distinct().ToList();
            foreach (var subjectId in distinctSubjectIds)
            {
                Subject subject = _dbContext.Subjects.Where(e => e.SubjectId == subjectId).FirstOrDefault();
                subjects.Add(subject);
            }
            
                
            return subjects;
        }

        
        public IEnumerable<LessonFile> GetAllLessonNonAprovedFile()
        {
            return _dbContext.LessonsFiles.Where(e => e.Approve==false).ToList();
        }

        public IEnumerable<LessonFile> GetAllLessonFile()
        {
            return _dbContext.LessonsFiles.ToList();
        }

        public void AproveLessonFile(string lessonFileId)
        {
            var lessonFile = _dbContext.LessonsFiles.Find(lessonFileId);
            lessonFile.Approve= true;
            Save();
        }

        public void UnAproveLessonFile(string lessonFileId)
        {
            var lessonFile = _dbContext.LessonsFiles.Find(lessonFileId);
            lessonFile.Approve = false;
            Save();
        }

        public IEnumerable<LessonFile> GetLessonFileByName(string lessonFileName)
        {
            return _dbContext.LessonsFiles.Where(e => (
            _dbContext.FuzzySearch(e.LessonFileName) == _dbContext.FuzzySearch(lessonFileName)))
                .ToList();
        }

        public IEnumerable<LessonFile> GetAllLessonFileSorted(string sortby)
        {
            switch (sortby)
            {
                case "name": return _dbContext.LessonsFiles.OrderBy(e => e.SubjectId).ToList(); break;
                case "teacher": return _dbContext.LessonsFiles.OrderBy(e => e.TeacherName).ToList(); break;
                case "approve": return _dbContext.LessonsFiles.OrderBy(e => e.Approve).ToList(); break;
                default: return _dbContext.LessonsFiles.ToList();
            }
        }
    }
}
