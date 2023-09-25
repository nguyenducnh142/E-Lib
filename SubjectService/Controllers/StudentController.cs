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


        //SubjectList
        // GET: api/<SubjectController>
        [HttpGet("AllSubject")]
        public IActionResult GetSubjectList()
        {
            var subjects = _studentRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        //SearchSubjectBySubjectName/TeacherName
        // GET api/<SubjectController>/5
        [HttpGet("SearchSubject/{subjectName}")]
        public IActionResult GetByName(string subjectName)
        {
            var subject = _studentRepository.GetSubjectByName(subjectName);
            return new OkObjectResult(subject);
        }

        //SortByName
        // GET: api/<SubjectController>
        [HttpGet("SortSubjects")]
        public IActionResult SubjectListSort()
        {
            var subjects = _studentRepository.GetSubjectSorted();
            return new OkObjectResult(subjects);
        }

        //SortByLastAccess ?

        //StarFilter ?

        //StarSign ?

        //GetSubjectDescription
        // GET api/<SubjectController>/5
        [HttpGet("SubjectDescription/{id}")]
        public IActionResult GetSubjectDescription(string id)
        {
            var subjectDescription = _studentRepository.GetSubjectDescription(id);
            return new OkObjectResult(subjectDescription);
        }

        //LessonList
        [HttpGet("AllLesson/{subjectId}")]
        public IActionResult GetLessonList(string subjectId)
        {
            var lesson = _studentRepository.GetAllLesson(subjectId);
            return new OkObjectResult(lesson);
        }

        //LessonFileList
        [HttpGet("AllLessonFile/{lessonId}")]
        public IActionResult GetLessonFileList(string lessonId)
        {
            var lessonFile = _studentRepository.GetAllLessonFile(lessonId);
            return new OkObjectResult(lessonFile);
        }

        //DownLessonFile
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

        //QuestionList(AllLesson)
        [HttpGet("AllQuestion")]
        public IActionResult GetQuestionList(string subjectId)
        {
            var question = _studentRepository.GetAllQuestion(subjectId);
            return new OkObjectResult(question);
        }

        //QuestionList(GetByLesson)
        [HttpGet("LessonQuestion")]
        public IActionResult GetLessonQuestionList(string subjectId, string lessonId)
        {
            var question = _studentRepository.GetLessonQuestion(subjectId, lessonId);
            return new OkObjectResult(question);
        }

        //AnswerList
        [HttpGet("AllAnswer")]
        public IActionResult GetLessonQuestionList(string questionId)
        {
            var answer = _studentRepository.GetAnswer(questionId);
            return new OkObjectResult(answer);
        }

        //PostQuestion
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] Question question)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.InsertQuestion(question);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjectList), new { id = question.LessonId }, question);
            }
        }

        //PostAnswer
        [HttpPost("AddAnswer")]
        public IActionResult AddAnswer([FromBody] Answer answer)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.InsertAnswer(answer);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjectList), new { id = answer.AnswerId }, answer);
            }
        }

        //GetSubjectNotification
        [HttpGet("AllNotification")]
        public IActionResult GetSubjectNotification(string subjectId)
        {
            var subjectNoti = _studentRepository.GetSubjectNoti(subjectId);
            return new OkObjectResult(subjectNoti);
        }

    }
}
