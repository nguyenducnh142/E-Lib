using SubjectService.Models;

namespace SubjectService.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectByID(int subjectId);
        void InsertSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);
        void Save();
    }
}
