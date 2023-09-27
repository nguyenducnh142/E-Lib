using ExamService.Models;

namespace ExamService.Repository
{
    public interface IExamRepository
    {
        Task ChangeExamName(string examId, string examName);
        Task ChangeTNQuestionDetail(string questionId, string questionDetail);
        Task DeleteQuestion(string questionId);
        Task<IEnumerable<Exam>> GetExams();
        Task<IEnumerable<TNQuestion>> GetTNQuestions();
        Task<IEnumerable<Exam>> GetExamByName(string examName);
        Task<IEnumerable<Exam>> GetExamBySubject(string subjectId);
        Task<Exam> GetExamDetail(string examId);
        Task<IEnumerable<TNQuestion>> GetExamQuestions(string examId);
        Task<TNQuestion> GetTNQuestion(string questionId);
        Task AddExam(Exam exam);
        Task AddQuestionFromBank(string examId, int lowLevel, int medLevel, int highLevel);
        Task AddTLQuestion(TLQuestion tLQuestion);
        Task AddTNQuestion(TNQuestion tNQuestion);
    }
}
