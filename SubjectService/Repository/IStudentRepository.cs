using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Subject>> GetSubjects(string classId);
        Task<IEnumerable<Subject>> GetStarSubjects(string userId);
        Task<IEnumerable<Subject>> SearchSubjects(string searchInfo);
        Task<IEnumerable<Subject>> GetSubjectSorted(string classId);
        Task StarSubject(string subjectId, string userId);
        Task<string> GetSubjectDescription(string subjectId);
        Task<IEnumerable<Lesson>> GetLessons(string subjectId);
        Task<IEnumerable<LessonFile>> GetLessonFilesByLesson(string lessonId);
        Task<IEnumerable<LessonFile>> GetLessonFilesBySubject(string subjectId);

        Task<IEnumerable<Question>> GetAllQuestion(string subjectId);
        Task<IEnumerable<Question>> GetLessonQuestion(string subjectId, string lessonId);
        Task<IEnumerable<Answer>> GetAnswer(string questionId);
        Task InsertQuestion(Question question);
        Task InsertAnswer(Answer answer);
        Task<IEnumerable<SubjectNotification>> GetSubjectNoti(string subjectId);
    }
}
