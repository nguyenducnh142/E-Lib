using ExamService.Models;

namespace ExamService.Repository
{
    public interface ITeacherRepository
    {
        void ChangeExamName(string examId, string examName);
        void ChangeQuestionDetail(string questionId, string questionDetail);
        void DeleteQuestion(string questionId);
        IEnumerable<Exam> GetAllExam();
        IEnumerable<TNQuestion> GetAllTNQuestion();
        IEnumerable<Exam> GetExamByName(string examName);
        IEnumerable<Exam> GetExamBySubject(string subjectId);
        Exam GetExamDetail(string examId);
        IEnumerable<TNQuestion> GetExamQuestion(string examId);
        TNQuestion GetTNQuestion(string questionId);
        void InsertExam(Exam exam);
        void InsertQuestionFromBank(string examId, int lowLevel, int medLevel, int highLevel);
        void InsertTLQuestion(TLQuestion tLQuestion);
        void InsertTNQuestion(TNQuestion tNQuestion);
    }
}
