using ExamService.DbContexts;

namespace ExamService.Repository
{
    public class LeadershipRepository : ILeadershipRepository
    {
        private readonly ExamContext _dbContext;

        public LeadershipRepository(ExamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ChangeApproveExam(string examId)
        {
            var exam = _dbContext.Exams.Find(examId);
            if (exam.Approve == false)
            {
                exam.Approve = true;
            }
            else exam.Approve = false;
            _dbContext.SaveChanges();
        }
    }
}
