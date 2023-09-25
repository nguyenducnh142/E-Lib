using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ILeadershipRepository
    {
        IEnumerable<Subject> GetSubjects();
        IEnumerable<LessonFile> GetAllLessonNonAprovedFile();
        IEnumerable<LessonFile> GetAllLessonFile();
        void AproveLessonFile(string lessonFileId);
        void UnAproveLessonFile(string lessonFileId);
        IEnumerable<LessonFile> GetLessonFileByName(string lessonFileName);
        IEnumerable<LessonFile> GetAllLessonFileSorted(string sortby);
    }
}
