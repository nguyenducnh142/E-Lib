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


        public void UpdateSubDes(string subjectDesciption, int subjectId)
        {
            var subDes = new Subject(){
                SubjectId = subjectId,
                SubjectDescription = subjectDesciption
            };

            _dbContext.Subjects.Attach(subDes);
            _dbContext.Subjects.Entry(subDes).Property(x => x.SubjectDescription).IsModified = true;
            Save();
            
        }

        public void UpdateLesson(string lessonName, int lessonId)
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

        public void DeleteLessonFile(string lessonFileName)
        {

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", lessonFileName);
            File.SetAttributes(filepath, FileAttributes.Normal);
            File.Delete(exactpath);

        }

        public void UpdateLessonFile(string lessonFileName, int lessonFileId)
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

        public void InsertLesson(Lesson lesson)
        {
            _dbContext.Add(lesson);
            Save();
        }

        public string WriteFile(IFormFile file, string lessonFileName, int lessonId, string lessonFileDescription)
        {
            LessonFile lessonFile = new LessonFile();
            lessonFile.LessonId = lessonId;
            lessonFile.LessonFileName = lessonFileName;
            lessonFile.LessonFileDescription = lessonFileDescription;
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
    }
}
