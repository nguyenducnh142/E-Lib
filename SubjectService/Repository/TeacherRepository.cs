using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
//using SubjectService.Migrations;
using SubjectService.Models;

namespace SubjectService.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SubjectContext _dbContext;

        public TeacherRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public async Task UpdateSubDes(string subjectDesciption, string subjectId)
        {
            var subDes = new Subject(){
                SubjectId =  subjectId,
                SubjectDescription = subjectDesciption
            };

            _dbContext.Subjects.Attach(subDes);
            _dbContext.Subjects.Entry(subDes).Property(x => x.SubjectDescription).IsModified = true;
            Save();
            
        }

        public async Task UpdateLesson(string lessonName, string lessonId)
        {
            var lesson = new Lesson()
            {
                LessonId = lessonId,
                LessonName = lessonName
            };

            _dbContext.Lessons.Attach(lesson);
            _dbContext.Lessons.Entry(lesson).Property(x => x.LessonName).IsModified = true;
            Save();
        }

        public async Task DeleteLessonFile(string lessonFileId)
        {

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", lessonFileId);
            File.SetAttributes(filepath, FileAttributes.Normal);
            File.Delete(exactpath);

        }

        public async Task UpdateLessonFile(string lessonFileName, string lessonFileId)
        {
            var lessonFile = new LessonFile()
            {
                LessonFileId = lessonFileId,
                LessonFileName = lessonFileName
            };

            _dbContext.LessonsFiles.Attach(lessonFile);
            _dbContext.LessonsFiles.Entry(lessonFile).Property(x => x.LessonFileName).IsModified = true;
            Save();
        }

        public async Task InsertLesson(Lesson lesson)
        {
            _dbContext.Add(lesson);
            Save();
        }

        public async Task WriteFile(IFormFile file,string lessonFileId, string lessonFileName, string lessonId, string lessonFileDescription)
        {
            LessonFile lessonFile = new LessonFile();
            lessonFile.LessonFileId = lessonFileId;
            lessonFile.LessonId = lessonId;
            lessonFile.LessonFileName = lessonFileName;
            lessonFile.LessonFileDescription = lessonFileDescription;
            lessonFile.SubjectId = _dbContext.Lessons.Find(lessonId).SubjectId;
            lessonFile.TeacherName = _dbContext.Subjects.Find(lessonFile.SubjectId).TeacherId;
            _dbContext.Add(lessonFile);
            Save();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
                filename = lessonFile.LessonFileId + extension;

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

        public async Task<IEnumerable<Subject>> GetSubjects(string teacherId)
        {
            var subjects = _dbContext.Subjects.Where(e => e.TeacherId == teacherId).ToList();
            return subjects;
        }

        public async Task<IEnumerable<Subject>> SearchSubjects(string searchInfo)
        {
            var subjects = _dbContext.Subjects.Where(e => (
            _dbContext.FuzzySearch(e.SubjectName) == _dbContext.FuzzySearch(searchInfo) |
            _dbContext.FuzzySearch(e.SubjectId) == _dbContext.FuzzySearch(searchInfo)))
                .ToList();
            return subjects;
        }

        public async Task<IEnumerable<Subject>> SortedSubjects(string teacherId)
        {
            var subjects = await GetSubjects(teacherId);
            return subjects.OrderBy(e => e.SubjectName).ToList();
        }

        public async Task<IEnumerable<Lesson>> GetLessons(string subjectId)
        {
            return _dbContext.Lessons.Where(e => e.SubjectId == subjectId).ToList();
        }

        public async Task<IEnumerable<LessonFile>> GetLessonFilesByLesson(string lessonId)
        {
            return _dbContext.LessonsFiles.Where(e => e.LessonId == lessonId && e.Approve == true).ToList();
        }

        public async Task<IEnumerable<LessonFile>> GetLessonFilesBySubject(string subjectId)
        {
            var lessonFiles = new List<LessonFile>();
            var lessons = await GetLessons(subjectId);
            foreach (var lesson in lessons)
            {
                lessonFiles.Add(_dbContext.LessonsFiles.Where(e => e.LessonId == lesson.LessonId && e.Approve == true).FirstOrDefault());
            }
            return lessonFiles;
        }

        public async Task<IEnumerable<SubjectClass>> GetClass(string teacherId)
        {
            var subjects = await GetSubjects(teacherId);
            var subjectClasses = new List<SubjectClass>();
            foreach (var subject in subjects)
            {
                subjectClasses.Add(_dbContext.SubjectClasses.Where(e => e.SubjectId == subject.SubjectId).FirstOrDefault());
            }
            return subjectClasses;
        }

        public async Task InsertSubjectNoti(SubjectNotification subjectNotification)
        {
            _dbContext.Add(subjectNotification);
            Save();
        }
    }
}
