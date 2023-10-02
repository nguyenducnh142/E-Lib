using Microsoft.AspNetCore.Mvc;
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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly NotificationService.Repository.INotificationRepository _notificationRepository;

        public TeacherController(ITeacherRepository teacherRepository, NotificationService.Repository.INotificationRepository notificationRepository = null, IStudentRepository studentRepository = null)
        {
            _teacherRepository = teacherRepository;
            _notificationRepository = notificationRepository;
            _studentRepository = studentRepository;
        }
        //Get Current UserDetail
        private string GetUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("name"));
            return id;
        }

        //Get Subjects 
        [HttpGet("GetSubjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _teacherRepository.GetSubjects(GetUserId());
            return new OkObjectResult(subjects);
        }

        //Search Subjects By SearchInfo(subjectId/SubjectName)
        [HttpGet("SearchSubject")]
        public IActionResult SearchSubjects(string searchInfo)
        {
            var subjects = _teacherRepository.SearchSubjects(searchInfo);
            return new OkObjectResult(subjects);
        }

        //Sort Subject By Name
        [HttpGet("SortSubjects")]
        public IActionResult SortSubjects()
        {
            var subjects = _teacherRepository.SortedSubjects(GetUserId());
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("GetLessons")]
        public IActionResult GetLessons(string subjectId)
        {
            var lesson = _teacherRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFile By LessonId
        [HttpGet("GetLessonFilesByLesson")]
        public IActionResult GetLessonFiles(string lessonId)
        {
            var lessonFile = _teacherRepository.GetLessonFilesByLesson(lessonId);
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFile By SubjectId
        [HttpGet("GetLessonFilesBySubject")]
        public IActionResult GetLessonFilesBySubject(string subjectId)
        {
            var lessonFile = _teacherRepository.GetLessonFilesBySubject(subjectId);
            return new OkObjectResult(lessonFile);
        }

        //Change SubjectDescription
        [HttpPut("UpdateSubjectDescription")]
        public IActionResult UpdateSubDescrip([FromBody] string subjectDecription, string subjectId)
        {
            if (subjectDecription != null && subjectId != null)
            {
                using (var scope = new TransactionScope())
                {
                    _teacherRepository.UpdateSubDes(subjectDecription, subjectId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        //Change LessonName
        [HttpPut("UpdateLessonName")]
        public IActionResult UpdateLesson([FromBody] string lessonName, string lessonId)
        {
            if (lessonName != null && lessonId != null)
            {
                using (var scope = new TransactionScope())
                {
                    _teacherRepository.UpdateLesson(lessonName, lessonId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        //Delete LessonFile (Filename + đuôi)
        [HttpDelete("DeleteLessonFile")]
        public IActionResult DeleteFile(string lessonFileId)
        {
            _teacherRepository.DeleteLessonFile(lessonFileId);
            return new OkResult();
        }

        //Update LessonFileDesciption
        [HttpPut("UpdateLessonFileName")]
        public IActionResult UpdateLessonFileName(string lessonFileName, string lessonFileId)
        {
            if (lessonFileName != null && lessonFileId != null)
            {
                using (var scope = new TransactionScope())
                {
                    _teacherRepository.UpdateLessonFile(lessonFileName, lessonFileId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        //AddLesson
        [HttpPost("AddLesson")]
        public IActionResult AddLesson(Lesson lesson)
        {
            using (var scope = new TransactionScope())
            {
                _teacherRepository.InsertLesson(lesson);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = lesson.LessonId }, lesson);
            }
        }

        //Add LessonFile
        [HttpPost("UploadLessonFile")]
        public IActionResult UploadLessonFile(IFormFile file, string lessonFileId, string lessonFileName, string lessonId, string lessonDescription)
        {
            _teacherRepository.WriteFile(file, lessonFileId, lessonFileName, lessonId, lessonDescription);
            return new OkObjectResult(lessonFileName);
        }


        //Get Class
        [HttpGet("GetClass")]
        public IActionResult GetClass()
        {
            var subjects = _teacherRepository.GetClass(GetUserId());
            return new OkObjectResult(subjects);
        }

        //Add Subject Notification
        [HttpPost("AddSubjectNotification")]
        public IActionResult AddNotification(SubjectNotification subjectNotification)
        {
            using (var scope = new TransactionScope())
            {
                _teacherRepository.InsertSubjectNoti(subjectNotification);
                _notificationRepository.AddNoti(subjectNotification.SubjectId, "Môn học" + subjectNotification.SubjectId + "có 1 thông báo mới!!!");
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = subjectNotification.SubjectNotificationId }, subjectNotification);
            }
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
        public IActionResult GetLessonQuestionList( string questionId)
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
        public IActionResult AddAnswer([FromBody] Answer answer)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.InsertAnswer(answer);
                _notificationRepository.AddNoti(GetSubjectId(answer.QuestionId), "Môn học " + GetSubjectId(answer.QuestionId) + " có câu trả lời mới");
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = answer.AnswerId }, answer);
            }
        }
    }
}
