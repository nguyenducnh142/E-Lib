using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SubjectService.Models;
using SubjectService.Repository;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        //Get Subjects By Class
        [HttpGet("/GetSubjects")]
        public async Task<IActionResult> GetSubjects(string classId)
        {
            var subjects = await _studentRepository.GetSubjects(classId);
            return new OkObjectResult(subjects);
        }

        //Get Star Subjects
        [HttpGet("/GetStarSubjects")]
        public async Task<IActionResult> GetStarSubjects(string userId)
        {
            var subjects = await _studentRepository.GetStarSubjects( userId);
            return new OkObjectResult(subjects);
        }

        //Search Subject By SubjectName or TeacherName
        [HttpGet("/SearchSubjects/{subjectName}")]
        public async Task<IActionResult> GetByName(string searchInfo)
        {
            var subject = await _studentRepository.SearchSubjects(searchInfo);
            return new OkObjectResult(subject);
        }

        //Sort Subjects By Name
        [HttpGet("/SortSubjects")]
        public async Task<IActionResult> SubjectsSort(string classId)
        {
            var subjects = await _studentRepository.GetSubjectSorted(classId);
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("/GetLessons/{subjectId}")]
        public async Task<IActionResult> GetLessonList(string subjectId)
        {
            var lesson = await _studentRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFile By LessonId
        [HttpGet("/GetLessonFiles/{lessonId}")]
        public async Task<IActionResult> GetLessonFileList(string lessonId)
        {
            var lessonFile = await _studentRepository.GetLessonFilesByLesson(lessonId);
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFile By SubjectId
        [HttpGet("/GetLessonFiles/{subjectId}")]
        public async Task<IActionResult> GetLessonFileListBySubject(string subjectId)
        {
            var lessonFile = await _studentRepository.GetLessonFilesBySubject(subjectId);
            return new OkObjectResult(lessonFile);
        }

        //Downloead LessonFile
        [HttpGet]
        [Route("/DownloadFile")]
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
        [HttpPut("/StarSubject")]
        public async Task<IActionResult> AproveLessonFile(string subjectId, string userId)
        {
            using (var scope = new TransactionScope())
            {
                await _studentRepository.StarSubject(subjectId, userId);
                scope.Complete();
                return new OkResult();
            }
        }

        //Get Subject Description
        [HttpGet("/GetSubjectDescription/{subjectId}")]
        public async Task<IActionResult> GetSubjectDescription(string subjectId)
        {
            var subjectDescription = await _studentRepository.GetSubjectDescription(subjectId);
            return new OkObjectResult(subjectDescription);
        }


        //Get Questions By SubjectId
        [HttpGet("/AllQuestion")]
        public async Task<IActionResult> GetQuestionList(string subjectId)
        {
            var question = await _studentRepository.GetAllQuestion(subjectId);
            return new OkObjectResult(question);
        }

        //Get Questions By LessionId
        [HttpGet("/LessonQuestion")]
        public async Task<IActionResult> GetLessonQuestionList(string subjectId, string lessonId)
        {
            var question = await _studentRepository.GetLessonQuestion(subjectId, lessonId);
            return new OkObjectResult(question);
        }

        //Get Answer By QuestionId
        [HttpGet("/AllAnswer")]
        public async Task<IActionResult> GetLessonQuestionList(string questionId)
        {
            var answer = await _studentRepository.GetAnswer(questionId);
            return new OkObjectResult(answer);
        }

        //Add Question
        [HttpPost("/AddQuestion")]
        public async Task<IActionResult> AddQuestion([FromBody] Question question)
        {
            using (var scope = new TransactionScope())
            {
                await _studentRepository.InsertQuestion(question);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = question.LessonId }, question);
            }
        }

        //Add Answer
        [HttpPost("/AddAnswer")]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            using (var scope = new TransactionScope())
            {
                await _studentRepository.InsertAnswer(answer);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = answer.AnswerId }, answer);
            }
        }

        //Get Subject Notifications
        [HttpGet("/AllNotification")]
        public async Task<IActionResult> GetSubjectNotification(string subjectId)
        {
            var subjectNoti = await _studentRepository.GetSubjectNoti(subjectId);
            return new OkObjectResult(subjectNoti);
        }

    }
}
