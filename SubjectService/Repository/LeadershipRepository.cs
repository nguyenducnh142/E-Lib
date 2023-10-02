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

        public async Task<IEnumerable<Subject>> GetSubjectsNonAproved()
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

        
        public async Task<IEnumerable<LessonFile>> GetLessonFilesNonAproved()
        {
            return _dbContext.LessonsFiles.Where(e => e.Approve==false).ToList();
        }

        public async Task<IEnumerable<LessonFile>> GetLessonFiles()
        {
            return _dbContext.LessonsFiles.ToList();
        }

        public async Task AproveLessonFile(string lessonFileId)
        {
            var lessonFile = _dbContext.LessonsFiles.Find(lessonFileId);
            if (lessonFile.Approve == false)
            {
                lessonFile.Approve = true;
            }
            else
            {
                lessonFile.Approve = false;
            }
            Save();
        }


        public async Task<IEnumerable<LessonFile>> GetLessonFileByName(string lessonFileName)
        {
            var lessonFiles = _dbContext.LessonsFiles.Where(e => (
            _dbContext.FuzzySearch(e.LessonFileName) == _dbContext.FuzzySearch(lessonFileName)))
                .ToList();
            return lessonFiles;
        }

        public async Task<IEnumerable<LessonFile>> SortLessonFiles(string sortby)
        {
            switch (sortby)
            {
                case "name": return _dbContext.LessonsFiles.OrderBy(e => e.SubjectId).ToList();
                case "teacher": return _dbContext.LessonsFiles.OrderBy(e => e.TeacherName).ToList();
                case "approve": return _dbContext.LessonsFiles.OrderBy(e => e.Approve).ToList();
                default: return _dbContext.LessonsFiles.ToList();
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return _dbContext.Subjects.ToList();
        }

        public async Task<IEnumerable<Lesson>> GetLessons(string subjectId)
        {
            return _dbContext.Lessons.ToList();
        }

        public async Task InsertSubject(Subject subject)
        {
            _dbContext.Add(subject);
            Save();
        }

        public string GetSubjectId(string lessonFileId)
        {
            return _dbContext.LessonsFiles.Find(lessonFileId).SubjectId;
        }

        public void AddSubjectIntoClass(string subjectid, string classId)
        {
            var tmp = _dbContext.SubjectClasses.Where(e => e.SubjectId == subjectid && e.ClassId == classId).FirstOrDefault();
            if (tmp == null)
            {
                var subjectClass = new SubjectClass();
                subjectClass.SubjectId = subjectid;
                subjectClass.ClassId = classId;
                _dbContext.Add(subjectClass);
                Save();
            }
        }
    }
}
