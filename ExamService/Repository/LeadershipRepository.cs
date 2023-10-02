using ExamService.DbContexts;
using ExamService.Models;
using System.Diagnostics;

namespace ExamService.Repository
{
    public class LeadershipRepository : ILeadershipRepository
    {
        private readonly ExamContext _dbContext;

        public LeadershipRepository(ExamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeApproveExam(string examId)
        {
            var exam = _dbContext.Exams.Find(examId);
            if (exam.Approve == false)
            {
                exam.Approve = true;
            }
            else exam.Approve = false;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Exam> GetExamByName(string examName)
        {
            return _dbContext.Exams.Where(e => _dbContext.FuzzySearch(e.ExamName) == _dbContext.FuzzySearch(examName));
        }

        public IEnumerable<Exam> GetExams()
        {
            return _dbContext.Exams.ToList();
        }

        public async Task<IEnumerable<Exam>> SortExams(string sort)
        {
            switch (sort)
            {
                case "subject": return _dbContext.Exams.OrderBy(e=>e.SubjectId).ToList();
                case "aprove":return _dbContext.Exams.OrderBy(e=>e.Approve).ToList();
                case "teacher": return _dbContext.Exams.OrderBy(e => e.TeacherId).ToList();
                default: return _dbContext.Exams.ToList();
            }
        }
    }
}
