using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ITeacherRepository
    {
        Task UpdateSubDes(string subjectDesciption, string subjectId);
        Task UpdateLesson(string lessonName, string lessonId);
        Task DeleteLessonFile(string fileName);
        Task UpdateLessonFile(string lessonFileName, string lessonFileId);
        Task InsertLesson(Lesson lesson);
        Task WriteFile(IFormFile file, string lessonFileName, string lessonId, string lessonFileDescription);
        Task<IEnumerable<Subject>> GetSubjects(string teacherName);
        Task<IEnumerable<Subject>> SearchSubjects(string searchInfo);
        Task<IEnumerable<Subject>> SortedSubjects(string teacherName);
        Task<IEnumerable<Lesson>> GetLessons(string subjectId);
        Task<IEnumerable<LessonFile>> GetLessonFilesByLesson(string lessonId);
        Task<IEnumerable<LessonFile>> GetLessonFilesBySubject(string subjectId);
        Task<IEnumerable<SubjectClass>> GetClass(string teacherName);
        Task InsertSubjectNoti(SubjectNotification subjectNotification);
    }
}
