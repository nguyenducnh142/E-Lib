using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
//using SubjectService.Migrations;
using SubjectService.Models;
using SubjectService.Repository;
using System.Security.Claims;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly NotificationService.Repository.INotificationRepository _notificationRepository;

        public StudentController(IStudentRepository studentRepository, NotificationService.Repository.INotificationRepository notificationRepository = null)
        {
            _studentRepository = studentRepository;
            _notificationRepository = notificationRepository;
        }

        //Get Current UserDetail
        private string GetUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("name"));
            return id;
        }
        private string GetClass()
        {
            string userClass = _studentRepository.GetClass(GetUserId());
            return userClass;
        }


        //Get Subjects 
        [HttpGet("GetSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _studentRepository.GetSubjects(GetClass());
            return new OkObjectResult(subjects);
        }

        //Get Star Subjects
        [HttpGet("GetStarSubjects")]
        public IActionResult GetStarSubjects()
        {
            var subjects = _studentRepository.GetStarSubjects(GetUserId());
            return new OkObjectResult(subjects);
        }

        //Search Subject By SubjectName or TeacherName
        [HttpGet("SearchSubjects")]
        public IActionResult GetByName(string searchInfo)
        {
            var subject = _studentRepository.SearchSubjects(searchInfo, GetClass());
            return new OkObjectResult(subject);
        }

        //Sort Subjects By Name
        [HttpGet("SortSubjects")]
        public IActionResult SubjectsSort()
        {
            var subjects = _studentRepository.GetSubjectSorted(GetClass());
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("GetLessons")]
        public IActionResult GetLessonList(string subjectId)
        {
            var lesson = _studentRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFile By LessonId
        [HttpGet("GetLessonFilesByLesson")]
        public IActionResult GetLessonFileList(string lessonId)
        {
            var lessonFile = _studentRepository.GetLessonFilesByLesson(lessonId);
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFile By SubjectId
        [HttpGet("GetLessonFilesBySubject")]
        public IActionResult GetLessonFileListBySubject(string subjectId)
        {
            var lessonFile = _studentRepository.GetLessonFilesBySubject(subjectId);
            return new OkObjectResult(lessonFile);
        }

        //Download LessonFile
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }


        //Star Subject
        [HttpPut("StarSubject")]
        public IActionResult AproveLessonFile(string subjectId)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.StarSubject(subjectId, GetUserId());
                scope.Complete();
                return new OkResult();
            }
        }

        //Get Subject Description
        [HttpGet("GetSubjectDescription")]
        public IActionResult GetSubjectDescription(string subjectId)
        {
            var subjectDescription = _studentRepository.GetSubjectDescription(subjectId);
            return new OkObjectResult(subjectDescription);
        }


        //Get Questions By SubjectId
        [HttpGet("AllQuestion")]
        public IActionResult GetQuestionList(string subjectId)
        {
            var question = _studentRepository.GetAllQuestion(subjectId);
            return new OkObjectResult(question);
        }

        //Get Questions By LessionId
        [HttpGet("LessonQuestion")]
        public IActionResult GetLessonQuestionList(string subjectId, string lessonId)
        {
            var question = _studentRepository.GetLessonQuestion(subjectId, lessonId);
            return new OkObjectResult(question);
        }

        //Get SubjectId by QuestionId
        private string GetSubjectId(string questionId)
        {
            return _studentRepository.GetSubjectId(questionId);
        }

        //Get Answer By QuestionId
        [HttpGet("AllAnswer")]
        public IActionResult GetLessonQuestionList(string questionId)
        {
            var answer = _studentRepository.GetAnswer(questionId);
            return new OkObjectResult(answer);
        }

        //Add Question
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] Question question)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.InsertQuestion(question);
                _notificationRepository.AddNoti(question.SubjectId, "Môn học " + question.SubjectId + " có câu hỏi mới");
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = question.LessonId }, question);
            }
        }

        //Add Answer
        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            using (var scope = new TransactionScope())
            {
                await _studentRepository.InsertAnswer(answer);
                await _notificationRepository.AddNoti(GetSubjectId(answer.QuestionId), "Môn học " + GetSubjectId(answer.QuestionId) + " có câu trả lời mới");
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = answer.AnswerId }, answer);
            }
        }

        //Get Subject Notifications
        [HttpGet("SubjectNotification")]
        public IActionResult GetSubjectNotification(string subjectId)
        {
            var subjectNoti = _studentRepository.GetSubjectNoti(subjectId);
            return new OkObjectResult(subjectNoti);
        }

    }
}
