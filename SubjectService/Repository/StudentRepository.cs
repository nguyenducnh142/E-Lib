using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
//using SubjectService.Migrations;
using SubjectService.Models;

namespace SubjectService.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SubjectContext _dbContext;

        public StudentRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return _dbContext.Subjects.ToList();
        }

        public IEnumerable<Subject> GetSubjectByName(string subjectName)
        {
            return _dbContext.Subjects.Where(e => (
            _dbContext.FuzzySearch(e.SubjectName) == _dbContext.FuzzySearch(subjectName)|
            _dbContext.FuzzySearch(e.TeacherName) == _dbContext.FuzzySearch(subjectName)))
                .ToList();
        }

        public IEnumerable<Subject> GetSubjectSorted()
        {
            return _dbContext.Subjects.OrderBy(e => e.SubjectName).ToList();
        }
        /*public IEnumerable<Subject> GetSubjectSorted()
        {
            var subjects = _dbContext.Subjects.Where(e => e.Approve == true);
            subjects = subjects.OrderBy(e => e.SubjectName);
            return subjects.ToList();
        }*/

        public string GetSubjectDescription(int subjectId)
        {
            return _dbContext.Subjects.Find(subjectId).SubjectDescription;
        }

        public IEnumerable<Lesson> GetAllLesson(int subjectId)
        {
            return _dbContext.Lessons.Where(e=>e.SubjectId==subjectId).ToList();
        }

        public IEnumerable<LessonFile> GetAllLessonFile(int lessonId)
        {
            return _dbContext.LessonsFiles.Where(e => e.LessonId == lessonId && e.Approve == true).ToList();
        }

        public IEnumerable<Question> GetAllQuestion(int subjectId)
        {
            return _dbContext.Questions.Where(e => e.SubjectId == subjectId).ToList();
        }

        public IEnumerable<Question> GetLessonQuestion(int subjectId, int lessonId)
        {
            return _dbContext.Questions.Where(e => e.SubjectId == subjectId&&e.LessonId==lessonId).ToList();
        }

        public IEnumerable<Answer> GetAnswer(int questionId)
        {
            return _dbContext.Answers.Where(e => e.QuestionId == questionId).ToList();
        }

        public void InsertQuestion(Question question)
        {
            _dbContext.Add(question);
            Save();
        }

        public void InsertAnswer(Answer answer)
        {
            _dbContext.Add(answer);
            Save();
        }

        public IEnumerable<SubjectNotification> GetSubjectNoti(int subjectId)
        {
            return _dbContext.SubjectNotifications.Where(e => e.SubjectId == subjectId).ToList();
        }
    }
}
