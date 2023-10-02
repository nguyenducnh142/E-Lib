using ExamService.Models;

namespace ExamService.Repository
{
    public interface ILeadershipRepository
    {

        Task ChangeApproveExam(string examId);
        IEnumerable<Exam> GetExamByName(string examName);
        IEnumerable<Exam> GetExams();
        Task<IEnumerable<Exam>> SortExams(string sort);
    }
}
