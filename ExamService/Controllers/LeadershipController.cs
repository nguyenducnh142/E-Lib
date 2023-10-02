using ExamService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILeadershipRepository _leadershipRepository;

        public LeadershipController(ITeacherRepository teacherRepository, ILeadershipRepository leadershipRepository)
        {
            _teacherRepository = teacherRepository;
            _leadershipRepository = leadershipRepository;
        }

        // Get Exams
        [HttpGet("GetAllExam")]
        public IActionResult GetExams()
        {
            var exams = _leadershipRepository.GetExams();
            return new OkObjectResult(exams);

        }

        //Sort By Subject/Aprove/Teacher
        [HttpGet("SortExams")]
        public IActionResult SortExams(string sort)
        {
            var exams = _leadershipRepository.SortExams(sort);
            return new OkObjectResult(exams);
        }

        //Search Exam By Name
        [HttpGet("GetExamByName")]
        public IActionResult GetExamByName(string examName)
        {
            var exams = _leadershipRepository.GetExamByName(examName);
            return new OkObjectResult(exams);
        }

        //Get ExamDetail
        [HttpGet("GetExamDetail")]
        public IActionResult GetExamDetail(string examId)
        {
            var examDetail = _teacherRepository.GetExamDetail(examId);
            return new OkObjectResult(examDetail);
        }

        //Get Exam's Questions
        [HttpGet("GetExamQuestions")]
        public IActionResult GetExamQuestions(string examId)
        {
            var exam =_teacherRepository.GetExamDetail(examId);
            if (exam.ExamFormal == "Trac nghiem")
            {
                var questions = _teacherRepository.GetExamTNQuestions(examId);
                return new OkObjectResult(questions);
            }
            else
            {
                var questions =_teacherRepository.GetExamTLQuestions(examId);
                return new OkObjectResult(questions);
            }
        }

        //Aprove Exam
        [HttpPut("ChangeApproveExam")]
        public IActionResult ChangeApproveExam(string examId)
        {
            _leadershipRepository.ChangeApproveExam(examId);
            return new OkObjectResult(examId);
        }

    }
}
