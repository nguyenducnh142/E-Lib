using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
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

        public async Task<IEnumerable<Subject>> GetSubjects(string classId)
        {
            var subjects = new List<Subject>();
            var subjectIds = _dbContext.SubjectClasses.Where(e=> e.ClassId==classId).ToList();
            foreach (var subject in subjectIds)
            {
                subjects.Add(_dbContext.Subjects.Find(subject.SubjectId));
            }
            return subjects;
        }


        public async Task<IEnumerable<Subject>> GetStarSubjects(string userId)
        {
            var subjects = new List<Subject>();
            var starSubjects = _dbContext.StarSubjects.Where(e => e.UserId==userId).ToList();
            foreach(var starSubject in starSubjects)
            {
                subjects.Add(_dbContext.Subjects.Find(starSubject.SubjectId));
            }
            return subjects;
        }


        public async Task<IEnumerable<Subject>> SearchSubjects(string searchInfo)
        {
            return _dbContext.Subjects.Where(e => (
            _dbContext.FuzzySearch(e.SubjectName) == _dbContext.FuzzySearch(searchInfo) |
            _dbContext.FuzzySearch(e.TeacherName) == _dbContext.FuzzySearch(searchInfo)))
                .ToList();
        }

        public async Task<IEnumerable<Subject>> GetSubjectSorted(string classId)
        {
            var subjects = await GetSubjects(classId);
            return subjects.OrderBy(e => e.SubjectName).ToList();
        }

        public async Task StarSubject(string subjectId, string userId)
        {
            var subject = _dbContext.StarSubjects.Where(e => e.SubjectId==subjectId && e.UserId==userId).FirstOrDefault();
            if(subject == null)
            {
                var starSubject = new StarSubject();
                starSubject.SubjectId = subjectId;
                starSubject.UserId = userId;
                _dbContext.Add(starSubject);
                Save();
            }
            else
            {
                _dbContext.StarSubjects.Remove(subject);
                Save();
            }
            
        }

        public async Task<string> GetSubjectDescription(string subjectId)
        {
            return _dbContext.Subjects.Find(subjectId).SubjectDescription;
        }

        public async Task<IEnumerable<Lesson>> GetLessons(string subjectId)
        {
            return _dbContext.Lessons.Where(e=>e.SubjectId==subjectId).ToList();
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
                lessonFiles.Add(_dbContext.LessonsFiles.Where(e=> e.LessonId==lesson.LessonId && e.Approve == true).FirstOrDefault());
            }
            return lessonFiles;
        }

        public async Task<IEnumerable<Question>> GetAllQuestion(string subjectId)
        {
            return _dbContext.Questions.Where(e => e.SubjectId == subjectId).ToList();
        }

        public async Task<IEnumerable<Question>> GetLessonQuestion(string subjectId, string lessonId)
        {
            return _dbContext.Questions.Where(e => e.SubjectId == subjectId&&e.LessonId==lessonId).ToList();
        }

        public async Task<IEnumerable<Answer>> GetAnswer(string questionId)
        {
            return _dbContext.Answers.Where(e => e.QuestionId == questionId).ToList();
        }

        public async Task InsertQuestion(Question question)
        {
            _dbContext.Add(question);
            Save();
        }

        public async Task InsertAnswer(Answer answer)
        {
            _dbContext.Add(answer);
            Save();
        }

        public async Task<IEnumerable<SubjectNotification>> GetSubjectNoti(string subjectId)
        {
            return _dbContext.SubjectNotifications.Where(e => e.SubjectId == subjectId).ToList();
        }

    }
}
