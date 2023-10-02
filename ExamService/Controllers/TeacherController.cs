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

        // Get Exams
        [HttpGet("GetAllExam")]
        public IActionResult GetExams()
        {
            var exams = _teacherRepository.GetExams();
            return new OkObjectResult(exams);

        }

        //Get Exam By SubjectId
        [HttpGet("GetExamBySubject")]
        public ActionResult GetExamBySubject(string subjectId)
        {
            var exams =_teacherRepository.GetExamBySubject(subjectId);
            return new OkObjectResult(exams);
        }

        //Search Exam By Name
        [HttpGet("GetExamByName")]
        public IActionResult GetExamByName( string examName)
        {
            var exams = _teacherRepository.GetExamByName(examName);
            return new OkObjectResult(exams);
        }

        //Create New Exam
        [HttpPost("AddExam")]
        public IActionResult AddExam( Exam exam)
        {
             _teacherRepository.AddExam(exam);
            return new OkObjectResult(exam);
        }

        //Add TLQuestion(tự luận)
        [HttpPost("AddTLQuestion")]
        public IActionResult AddTLQuesiton( TLQuestion tLQuestion)
        {
            _teacherRepository.AddTLQuestion(tLQuestion);
            return new OkObjectResult(tLQuestion);
        }

        //Add TNQuestion(Trắc nghiệm)
        [HttpPost("AddTNQuestion")]
        public IActionResult InsertTNQuesiton( TNQuestion tNQuestion)
        {
            _teacherRepository.AddTNQuestion(tNQuestion);
            return new OkObjectResult(tNQuestion);
        }

        //Add TNQuestion Into Exam(Trắc nghiệm)
        [HttpPost("AddTNQuesitonIntoExam")]
        public IActionResult InsertTNQuesitonIntoExam(TNQuestion tNQuestion,string examId)
        {
            _teacherRepository.AddTNQuestionIntoExam(tNQuestion,examId);
            return new OkObjectResult(tNQuestion);
        }

        //Get ExamDetail
        [HttpGet("GetExamDetail")]
        public IActionResult GetExamDetail( string examId)
        {
            var examDetail = _teacherRepository.GetExamDetail(examId);
            return new OkObjectResult(examDetail);
        }

        //Change ExamName
        [HttpPut("ChangeExamName")]
        public IActionResult ChangeExamName(string examId, string examName)
        {
            _teacherRepository.ChangeExamName(examId, examName);
            return new OkObjectResult(examName);
        }

        //Get TNQuestions
        [HttpGet("GetTNQuestions")]
        public IActionResult GetTNQuestions()
        {
            var questions = _teacherRepository.GetTNQuestions();
            return new OkObjectResult(questions);

        }

        //Get TNQuestion By QuestionId
        [HttpGet("GetTNQuestion")]
        public IActionResult GetTNQuestion( string questionId)
        {
            var questions = _teacherRepository.GetTNQuestion(questionId);
            return new OkObjectResult(questions);

        }

        //Change TNQuestionDetail
        [HttpPut("ChangeTNQuestionDetail")]
        public IActionResult ChangeTNQuestionDetail( string questionId, TNQuestion tNQuestion)
        {
            _teacherRepository.ChangeTNQuestionDetail(questionId, tNQuestion);
            return new OkObjectResult(questionId);
        }

        //Delete Question
        [HttpDelete("DeleteQuestion")]
        public ActionResult DeleteQuestion(string questionId)
        {
            _teacherRepository.DeleteQuestion(questionId) ;
            return new OkObjectResult(questionId);
        }

        //Create Exam By QuestionBank
        [HttpPost("InsertQuestionFromBank")]
        public IActionResult AddQuesionFromBank( string examId, int lowLevelQuestion, int medLevelQuestion, int highLevelQuestion)
        {
            _teacherRepository.AddQuestionFromBank(examId, lowLevelQuestion, medLevelQuestion, highLevelQuestion);
            return new OkObjectResult(examId);
        }

        //Get Exam's Questions
        [HttpGet("GetExamQuestions")]
        public IActionResult GetExamQuestions( string examId )
        {
            var exam = _teacherRepository.GetExamDetail(examId);
            if (exam.ExamFormal == "Trac nghiem")
            {
                var questions = _teacherRepository.GetExamTNQuestions(examId);
                return new OkObjectResult(questions);
            }
            else
            {
                var questions = _teacherRepository.GetExamTLQuestions(examId);
                return new OkObjectResult(questions);
            }
        }
    }
}
