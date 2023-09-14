using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
using SubjectService.Models;

namespace SubjectService.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SubjectContext _dbContext;

        public SubjectRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = _dbContext.Subjects.Find(subjectId);
            _dbContext.Subjects.Remove(subject);
            Save();
        }

        public Subject GetSubjectByID(int subjectId)
        {
            return _dbContext.Subjects.Find(subjectId);
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return _dbContext.Subjects.ToList();
        }

        public void InsertSubject(Subject subject)
        {
            _dbContext.Add(subject);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateSubject(Subject subject)
        {
            _dbContext.Entry(subject).State=EntityState.Modified;
        }
    }
}
