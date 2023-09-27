using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ILeadershipRepository
    {
        Task<IEnumerable<Subject>> GetSubjects();
        Task<IEnumerable<LessonFile>> GetLessonFilesNonAproved();
        Task<IEnumerable<LessonFile>> GetLessonFiles();
        Task AproveLessonFile(string lessonFileId);
        Task<IEnumerable<LessonFile>> GetLessonFileByName(string lessonFileName);
        Task<IEnumerable<LessonFile>> SortLessonFiles(string sortby);
        Task<IEnumerable<Subject>> GetSubjectsNonAproved();
        Task<IEnumerable<Lesson>> GetLessons(string subjectId);
        Task InsertSubject(Subject subject);
    }
}
