using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Subject> GetSubjects();
        IEnumerable<Subject> GetSubjectByName(string subjectName);
        IEnumerable<Subject> GetSubjectSorted();
        string GetSubjectDescription(string subjectId);
        IEnumerable<Lesson> GetAllLesson(string subjectId);
        IEnumerable<LessonFile> GetAllLessonFile(string lessonId);

        IEnumerable<Question> GetAllQuestion(string subjectId);
        IEnumerable<Question> GetLessonQuestion(string subjectId, string lessonId);
        IEnumerable<Answer> GetAnswer(string questionId);
        void InsertQuestion(Question question);
        void InsertAnswer(Answer answer);
        IEnumerable<SubjectNotification> GetSubjectNoti(string subjectId);
    }
}
