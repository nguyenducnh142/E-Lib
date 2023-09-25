using Microsoft.AspNetCore.Mvc;
using SubjectService.Models;
using System.Runtime.CompilerServices;

namespace SubjectService.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectByID(string subjectId);
        IEnumerable<Subject> GetSubjectByName(string subjectName);
        void InsertSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(string subjectId);
        void InsertLesson(Lesson lesson);
        void Save();
        string WriteFile(IFormFile file, string lessonFileName, string lessonId, string lessonFileDescription);
        void InsertQuestion(Question question);
        void InsertSubjectNoti(SubjectNotification subjectNotification);
        void DeleteLessonFile(string lessonFileName);
    }
}
