using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Subject> GetSubjects();
        IEnumerable<Subject> GetSubjectByName(string subjectName);
        IEnumerable<Subject> GetSubjectSorted();
        string GetSubjectDescription(int subjectId);
        IEnumerable<Lesson> GetAllLesson(int subjectId);
        IEnumerable<LessonFile> GetAllLessonFile(int lessonId);

        IEnumerable<Question> GetAllQuestion(int subjectId);
        IEnumerable<Question> GetLessonQuestion(int subjectId, int lessonId);
        IEnumerable<Answer> GetAnswer(int questionId);
        void InsertQuestion(Question question);
        void InsertAnswer(Answer answer);
        IEnumerable<SubjectNotification> GetSubjectNoti(int subjectId);
    }
}
