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
        public async Task ChangeExamName(string examId, string examName)
        {
            _dbContext.Exams.Find(examId).ExamName = examName;
            Save();
        }

        public async Task ChangeTNQuestionDetail(string questionId, TNQuestion tNQuestion)
        {
            var question = _dbContext.TNQuestions.Find(questionId);
            question.QuestionDetail = tNQuestion.QuestionDetail;
            question.AnswerA = tNQuestion.AnswerA;
            question.AnswerB = tNQuestion.AnswerB;
            question.AnswerC = tNQuestion.AnswerC;
            question.AnswerD = tNQuestion.AnswerD;
            question.CorrectAnswer = tNQuestion.CorrectAnswer;
            Save();
        }

        public async Task DeleteQuestion(string questionId)
        {
            var question = _dbContext.TNQuestions.Find(questionId);
            _dbContext.TNQuestions.Remove(question);
            Save();
        }

        public async Task<IEnumerable<Exam>> GetExams()
        {
            return _dbContext.Exams.Where(e=>e.Approve==true).ToList();
        }

        public async Task<IEnumerable<TNQuestion>> GetTNQuestions()
        {
            return _dbContext.TNQuestions.ToList();
        }
        public async Task<IEnumerable<TLQuestion>> GetTLQuestions()
        {
            return _dbContext.TLQuestions.ToList();
        }

        public async Task<IEnumerable<Exam>> GetExamByName(string examName)
        {
            return _dbContext.Exams.Where(e => _dbContext.FuzzySearch(e.ExamName) == _dbContext.FuzzySearch(examName) && e.Approve==true);
        }

        public async Task<IEnumerable<Exam>> GetExamBySubject(string subjectId)
        {
            return _dbContext.Exams.Where(e => e.SubjectId == subjectId && e.Approve==true);
        }

        public Exam GetExamDetail(string examId)
        {
            return _dbContext.Exams.Find(examId);
        }

        public async Task<TNQuestion> GetTNQuestion(string questionId)
        {
            return _dbContext.TNQuestions.Find(questionId);
        }



        public async Task AddExam(Exam exam)
        {
            _dbContext.Add(exam);
            Save();
        }

        public async Task AddQuestionFromBank(string examId, int lowLevel, int medLevel, int highLevel)
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
                var tNQuestionExam = new TNQuestionExam();
                tNQuestionExam.QuestionId = question.QuestionId;
                tNQuestionExam.ExamId = examId;
                _dbContext.Add(tNQuestionExam);
            }
            Save();
        }

        public async Task AddTLQuestion(TLQuestion tLQuestion)
        {
            _dbContext.Add(tLQuestion);
            Save();
        }

        public async Task AddTNQuestion(TNQuestion tNQuestion)
        {
            _dbContext.Add(tNQuestion);
            Save();
        }

        public async Task<IEnumerable<TNQuestion>> GetExamTNQuestions(string examId)
        {
            var questions = new List<TNQuestion>();
            var tNQuestionExam = _dbContext.TNQuestionExams.Where(e => e.ExamId == examId).ToList();
            foreach(var question in tNQuestionExam)
            {
                questions.Add(_dbContext.TNQuestions.Find(question.QuestionId));
            }
            return questions;
        }

        public async Task<TLQuestion> GetExamTLQuestions(string examId)
        {
            var question = _dbContext.TLQuestions.Find(examId);
            return question;
        }

        public void AddTNQuestionIntoExam(TNQuestion tNQuestion, string examId)
        {
            _dbContext.Add(tNQuestion);
            var tNExam = new TNQuestionExam();
            tNExam.ExamId = examId;
            tNExam.QuestionId = tNQuestion.QuestionId;
            _dbContext.Add(tNExam);
            Save();
        }
    }
}
