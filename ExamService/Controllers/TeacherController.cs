using ExamService.Models;
using ExamService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet("GetAllExam")]
        public IActionResult GetAllExam()
        {
            var exams = _teacherRepository.GetAllExam();
            return new OkObjectResult(exams);

        }

        [HttpGet("GetExamBySubject")]
        public IActionResult GetExamBySubject(string subjectId)
        {
            var exams = _teacherRepository.GetExamBySubject(subjectId);
            return new OkObjectResult(exams);
        }

        [HttpGet("GetExamByName")]
        public IActionResult GetExamByName( string examName)
        {
            var exams = _teacherRepository.GetExamByName(examName);
            return new OkObjectResult(exams);
        }

        [HttpPost("InsertExam")]
        public IActionResult InsertExam( Exam exam)
        {
            _teacherRepository.InsertExam(exam);
            return new OkObjectResult(exam);
        }

        [HttpPost("InsertTLQuestion")]
        public IActionResult InsertTLQuesiton( TLQuestion tLQuestion)
        {
            _teacherRepository.InsertTLQuestion(tLQuestion);
            return new OkObjectResult(tLQuestion);
        }

        [HttpPost("InsertTNQuestion")]
        public IActionResult InsertTNQuesiton( TNQuestion tNQuestion)
        {
            _teacherRepository.InsertTNQuestion(tNQuestion);
            return new OkObjectResult(tNQuestion);
        }

        [HttpGet("GetExamDetail")]
        public IActionResult GetExamDetail( string examId)
        {
            var examDetail = _teacherRepository.GetExamDetail(examId);
            return new OkObjectResult(examDetail);
        }

        [HttpPut("ChangeExamName")]
        public IActionResult ChangeExamName(string examId, string examName)
        {
            _teacherRepository.ChangeExamName(examId, examName);
            return new OkObjectResult(examName);
        }

        [HttpGet("GetAllTNQuestion")]
        public IActionResult GetAllTNQuestion()
        {
            var questions = _teacherRepository.GetAllTNQuestion();
            return new OkObjectResult(questions);

        }

        [HttpGet("GetTNQuestionDetail")]
        public IActionResult GetTNQuestion( string questionId)
        {
            var questions = _teacherRepository.GetTNQuestion(questionId);
            return new OkObjectResult(questions);

        }

        [HttpPut("ChangeQuestionDetail")]
        public IActionResult ChangeQuestionDetail( string questionId, string questionDetail)
        {
            _teacherRepository.ChangeQuestionDetail(questionId, questionDetail);
            return new OkObjectResult(questionId);
        }

        [HttpDelete("DeleteQuestion")]
        public IActionResult DeleteQuestion( string questionId)
        {
            _teacherRepository.DeleteQuestion(questionId) ;
            return new OkObjectResult(questionId);
        }

        [HttpPost("InsertQuestionFromBank")]
        public IActionResult InsertQuesionFromBank( string examId, int lowLevelQuestion, int medLevelQuestion, int highLevelQuestion)
        {
            _teacherRepository.InsertQuestionFromBank(examId, lowLevelQuestion, medLevelQuestion, highLevelQuestion);
            return new OkObjectResult(examId);
        }

        [HttpGet("GetExamQuestions")]
        public IActionResult GetExamQuestion( string examId )
        {
            var questions = _teacherRepository.GetExamQuestion(examId) ;
            return new OkObjectResult(questions);
        }
    }
}
