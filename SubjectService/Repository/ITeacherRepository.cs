using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ITeacherRepository
    {
        void UpdateSubDes(string subjectDesciption, string subjectId);
        void UpdateLesson(string lessonName, string lessonId);
        void DeleteLessonFile(string fileName);
        void UpdateLessonFile(string lessonFileName, string lessonFileId);
        void InsertLesson(Lesson lesson);
        void WriteFile(IFormFile file, string lessonFileName, string lessonId, string lessonFileDescription);
    }
}
