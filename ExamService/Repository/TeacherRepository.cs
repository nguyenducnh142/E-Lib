using ExamService.DbContexts;
using ExamService.Models;
using System;

namespace ExamService.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ExamContext _dbContext;

        public TeacherRepository(ExamContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void ChangeExamName(string examId, string examName)
        {
            _dbContext.Exams.Find(examId).ExamName = examName;
            Save();
        }

        public void ChangeQuestionDetail(string questionId, string questionDetail)
        {
            _dbContext.TNQuestions.Find(questionId).QuestionDetail=questionDetail;
            Save();
        }

        public void DeleteQuestion(string questionId)
        {
            var question = _dbContext.TNQuestions.Find(questionId);
            _dbContext.TNQuestions.Remove(question);
            Save();
        }

        public IEnumerable<Exam> GetAllExam()
        {
            return _dbContext.Exams.ToList();
        }

        public IEnumerable<TNQuestion> GetAllTNQuestion()
        {
            return _dbContext.TNQuestions.Where(e=>e.QuestionUsed==false).ToList();
        }

        public IEnumerable<Exam> GetExamByName(string examName)
        {
            return _dbContext.Exams.Where(e => _dbContext.FuzzySearch(e.ExamName) == _dbContext.FuzzySearch(examName));
        }

        public IEnumerable<Exam> GetExamBySubject(string subjectId)
        {
            return _dbContext.Exams.Where(e => e.SubjectId == subjectId);
        }

        public Exam GetExamDetail(string examId)
        {
            return _dbContext.Exams.Find(examId);
        }

        public TNQuestion GetTNQuestion(string questionId)
        {
            return _dbContext.TNQuestions.Find(questionId);
        }

        public void InsertExam(Exam exam)
        {
            _dbContext.Add(exam);
            Save();
        }

        public void InsertQuestionFromBank(string examId, int lowLevel, int medLevel, int highLevel)
        {
            List<TNQuestion> questionList = new List<TNQuestion>();
            var lowQuestion = _dbContext.TNQuestions.Where(e=>e.QuestionType==1).OrderBy(r => Guid.NewGuid()).Take(lowLevel);
            foreach(var question in lowQuestion)
            {
                questionList.Add(question);
            }
            var medQuestion = _dbContext.TNQuestions.Where(e => e.QuestionType == 2).OrderBy(r => Guid.NewGuid()).Take(medLevel);
            foreach (var question in medQuestion)
            {
                questionList.Add(question);
            }
            var highQuestion = _dbContext.TNQuestions.Where(e => e.QuestionType == 3).OrderBy(r => Guid.NewGuid()).Take(highLevel);
            foreach (var question in highQuestion)
            {
                questionList.Add(question);
            }
            foreach (var question in questionList)
            {
                question.QuestionId = examId + question.QuestionId;
                question.QuestionUsed = true;
                question.ExamId = examId;
                _dbContext.Add(question);
            }
            Save();
        }

        public void InsertTLQuestion(TLQuestion tLQuestion)
        {
            _dbContext.Add(tLQuestion);
            Save();
        }

        public void InsertTNQuestion(TNQuestion tNQuestion)
        {
            _dbContext.Add(tNQuestion);
            Save();
        }

        public IEnumerable<TNQuestion> GetExamQuestion(string examId)
        {
            return _dbContext.TNQuestions.Where(e=>e.ExamId==examId).ToList();
        }
    }
}
