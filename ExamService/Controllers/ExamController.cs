using ExamService.Models;
using ExamService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _teacherRepository;

        public ExamController(IExamRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        // Get Exams
        [HttpGet("/GetAllExam")]
        public async Task<IActionResult> GetExams()
        {
            var exams = await _teacherRepository.GetExams();
            return new OkObjectResult(exams);

        }

        //Search Exam By SubjectId
        [HttpGet("/GetExamBySubject/{subjectId}")]
        public async Task<IActionResult> GetExamBySubject(string subjectId)
        {
            var exams = await _teacherRepository.GetExamBySubject(subjectId);
            return new OkObjectResult(exams);
        }

        //Search Exam By Name
        [HttpGet("GetExamByName/{examName}")]
        public async Task<IActionResult> GetExamByName( string examName)
        {
            var exams = await _teacherRepository.GetExamByName(examName);
            return new OkObjectResult(exams);
        }

        //Create New Exam
        [HttpPost("/AddExam")]
        public async Task<IActionResult> AddExam( Exam exam)
        {
            await _teacherRepository.AddExam(exam);
            return new OkObjectResult(exam);
        }

        //Add TLQuestion(tự luận)
        [HttpPost("/AddTLQuestion")]
        public async Task<IActionResult> AddTLQuesiton( TLQuestion tLQuestion)
        {
            await _teacherRepository.AddTLQuestion(tLQuestion);
            return new OkObjectResult(tLQuestion);
        }

        //Add TNQuestion(Trắc nghiệm)
        [HttpPost("/AddTNQuestion")]
        public async Task<IActionResult> InsertTNQuesiton( TNQuestion tNQuestion)
        {
            await _teacherRepository.AddTNQuestion(tNQuestion);
            return new OkObjectResult(tNQuestion);
        }

        //Get ExamDetail
        [HttpGet("/GetExamDetail/{examId}")]
        public async Task<IActionResult> GetExamDetail( string examId)
        {
            var examDetail = await _teacherRepository.GetExamDetail(examId);
            return new OkObjectResult(examDetail);
        }

        //Change ExamName
        [HttpPut("/ChangeExamName")]
        public async Task<IActionResult> ChangeExamName(string examId, string examName)
        {
            await _teacherRepository.ChangeExamName(examId, examName);
            return new OkObjectResult(examName);
        }

        //Get TNQuestions
        [HttpGet("/GetTNQuestions")]
        public async Task<IActionResult> GetTNQuestions()
        {
            var questions = await _teacherRepository.GetTNQuestions();
            return new OkObjectResult(questions);

        }

        //Get TNQuestion
        [HttpGet("/GetTNQuestion/{questionId}")]
        public async Task<IActionResult> GetTNQuestion( string questionId)
        {
            var questions = await _teacherRepository.GetTNQuestion(questionId);
            return new OkObjectResult(questions);

        }

        //Change TNQuestionDetail
        [HttpPut("/ChangeTNQuestionDetail")]
        public async Task<IActionResult> ChangeTNQuestionDetail( string questionId, string questionDetail)
        {
            await _teacherRepository.ChangeTNQuestionDetail(questionId, questionDetail);
            return new OkObjectResult(questionId);
        }

        //Delete Question
        [HttpDelete("/DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            await _teacherRepository.DeleteQuestion(questionId) ;
            return new OkObjectResult(questionId);
        }

        //Create Exam By QuestionBank
        [HttpPost("/InsertQuestionFromBank")]
        public async Task<IActionResult> AddQuesionFromBank( string examId, int lowLevelQuestion, int medLevelQuestion, int highLevelQuestion)
        {
            await _teacherRepository.AddQuestionFromBank(examId, lowLevelQuestion, medLevelQuestion, highLevelQuestion);
            return new OkObjectResult(examId);
        }

        //Get Exam's Questions
        [HttpGet("/GetExamQuestions")]
        public async Task<IActionResult> GetExamQuestions( string examId )
        {
            var questions = await _teacherRepository.GetExamQuestions(examId) ;
            return new OkObjectResult(questions);
        }
    }
}
