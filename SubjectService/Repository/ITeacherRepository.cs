using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ITeacherRepository
    {
        void UpdateSubDes(string subjectDesciption, int subjectId);
        void UpdateLesson(string lessonName, int lessonId);
        void DeleteLessonFile(string fileName);
        void UpdateLessonFile(string lessonFileName, int lessonFileId);
        void InsertLesson(Lesson lesson);
        string WriteFile(IFormFile file, string lessonFileName, int lessonId, string lessonFileDescription);
    }
}
