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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        // GET: api/<SubjectController>
        [HttpGet]
        public IActionResult Get()
        {
            var subjects = _subjectRepository.GetSubjects();
            return new OkObjectResult(subjects);

            
        }


        // GET api/<SubjectController>/5
        [HttpGet("SearchById/{id}")]
        public IActionResult Get(int id)
        {
            var subject = _subjectRepository.GetSubjectByID(id);
            return new OkObjectResult(subject);
        }

        // GET api/<SubjectController>/5
        [HttpGet("SearchByName/{subjectName}")]
        public IActionResult GetByName(string subjectName)
        {
            var subject = _subjectRepository.GetSubjectByName(subjectName);
            return new OkObjectResult(subject);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public IActionResult Post([FromBody] Subject subject)
        {
            using (var scope = new TransactionScope())
            {
                _subjectRepository.InsertSubject(subject);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = subject.SubjectId }, subject);
            }
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Subject subject)
        {
            if (subject != null)
            {
                using (var scope = new TransactionScope())
                {
                    _subjectRepository.UpdateSubject(subject);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subjectRepository.DeleteSubject(id);
            return new OkResult();
        }

        // POST api/<SubjectController>
        [HttpPost("AddLesson")]
        public IActionResult AddLesson([FromBody] Lesson lesson)
        {
            using (var scope = new TransactionScope())
            {
                _subjectRepository.InsertLesson(lesson);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = lesson.LessonId}, lesson);
            }
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file, string lessonFileName, int lessonId,string lessonDescription, CancellationToken cancellationtoken)
        {
            return Ok(_subjectRepository.WriteFile(file, lessonFileName, lessonId, lessonDescription));
        }

        

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


        // POST api/<SubjectController>
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] Question question)
        {
            using (var scope = new TransactionScope())
            {
                _subjectRepository.InsertQuestion(question);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = question.LessonId }, question);
            }
        }

        [HttpPost("AddSubjectNotification")]
        public IActionResult AddNotification([FromBody] SubjectNotification subjectNotification)
        {
            using (var scope = new TransactionScope())
            {
                _subjectRepository.InsertSubjectNoti(subjectNotification);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = subjectNotification.SubjectNotificationId}, subjectNotification);
            }
        }

        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(string lessonFileName)
        {
            _subjectRepository.DeleteLessonFile( lessonFileName);
            return new OkResult();
        }
    }
}
