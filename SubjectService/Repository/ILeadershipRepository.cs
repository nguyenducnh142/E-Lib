using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ILeadershipRepository
    {
        IEnumerable<Subject> GetSubjects();
        IEnumerable<LessonFile> GetAllLessonNonAprovedFile();
        IEnumerable<LessonFile> GetAllLessonFile();
        void AproveLessonFile(int lessonFileId);
        void UnAproveLessonFile(int lessonFileId);
        IEnumerable<LessonFile> GetLessonFileByName(string lessonFileName);
        IEnumerable<LessonFile> GetAllLessonFileSorted(string sortby);
    }
}
