using Microsoft.AspNetCore.Mvc;
using SubjectService.Migrations;
using SubjectService.Models;
using SubjectService.Repository;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILeadershipRepository _leadershipRepository;

        public LeadershipController(IStudentRepository studentRepository, ILeadershipRepository leadershipRepository)
        {
            _studentRepository = studentRepository;
            _leadershipRepository = leadershipRepository;
        }

        //Subject
        // GET: api/<SubjectController>
        [HttpGet("AllSubject")]
        public IActionResult GetSubjectList()
        {
            var subjects = _studentRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        //SubjectNonApprove
        // GET: api/<SubjectController>
        [HttpGet("AllSubjectNonAprove")]
        public IActionResult GetSubjectListNonAproved()
        {
            var subjects = _leadershipRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        //Lesson
        [HttpGet("AllLesson/{subjectId}")]
        public IActionResult GetLessonList(string subjectId)
        {
            var lesson = _studentRepository.GetAllLesson(subjectId);
            return new OkObjectResult(lesson);
        }

        //LessonFile
        [HttpGet("AllLessonFile")]
        public IActionResult GetLessonFileList()
        {
            var lessonFile = _leadershipRepository.GetAllLessonFile();
            return new OkObjectResult(lessonFile);
        }

        //LessonFileNonAproved
        [HttpGet("AllLessonFileNonApprove")]
        public IActionResult GetLessonFileNonAprovedList()
        {
            var lessonFile = _leadershipRepository.GetAllLessonNonAprovedFile();
            return new OkObjectResult(lessonFile);
        }

        //SortLessonFile
        [HttpGet("AllLessonFileSorted/{sortby}")]
        public IActionResult GetLessonFileListSorted(string sortby)
        {
            var lessonFile = _leadershipRepository.GetAllLessonFileSorted(sortby);
            return new OkObjectResult(lessonFile);
        }



        //AproveLessonFile
        [HttpPut("AproveLessonFile/{lessonFileId}")]
        public IActionResult AproveLessonFile(string lessonFileId)
        {
            using (var scope = new TransactionScope())
            {
                _leadershipRepository.AproveLessonFile(lessonFileId);
                scope.Complete();
                return new OkResult();
            }
        }

        //UnAproveLessonFile
        [HttpPut("UnAproveLessonFile/{lessonFileId}")]
        public IActionResult UnAproveLessonFile(string lessonFileId)
        {
            using (var scope = new TransactionScope())
            {
                _leadershipRepository.UnAproveLessonFile(lessonFileId);
                scope.Complete();
                return new OkResult();
            }
        }

        //SearchLessonFile
        [HttpGet("SearchLessonFile/{lessonFileName}")]
        public IActionResult GetLessonFileByName(string lessonFileName)
        {
            var lessonFile = _leadershipRepository.GetLessonFileByName(lessonFileName);
            return new OkObjectResult(lessonFile);
        }

    }
}
