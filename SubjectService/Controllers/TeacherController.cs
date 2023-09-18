using Microsoft.AspNetCore.Mvc;
using SubjectService.Models;
using SubjectService.Repository;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
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


        //UpdateSubjectDescription
        [HttpPut("UpdateSubjectDescription")]
        public IActionResult UpdateSubDescrip([FromBody] string subjectDecription, int subjectId)
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

        //UpdateLesson
        [HttpPut("UpdateLessonName")]
        public IActionResult UpdateLesson([FromBody] string lessonName, int lessonId)
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


        //LessonFile
        [HttpDelete("DeleteLessonFile")]
        public IActionResult DeleteFile(string lessonFileName)
        {
            _teacherRepository.DeleteLessonFile(lessonFileName);
            return new OkResult();
        }

        //UpdateLessonFileDesciption
        [HttpPut("UpdateLessonFileName")]
        public IActionResult UpdateLessonFileName([FromBody] string lessonFileName, int lessonFileId)
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

        //ViewAsStudent???


        //AddLesson
        [HttpPost("AddLesson")]
        public IActionResult AddLesson([FromBody] Lesson lesson)
        {
            using (var scope = new TransactionScope())
            {
                _teacherRepository.InsertLesson(lesson);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjectList), new { id = lesson.LessonId }, lesson);
            }
        }

        //AddLessonFile
        [HttpPost("UploadLessonFile")]
        public IActionResult UploadLessonFile(IFormFile file, string lessonFileName, int lessonId, string lessonDescription, CancellationToken cancellationtoken)
        {
            return Ok(_teacherRepository.WriteFile(file, lessonFileName, lessonId, lessonDescription));
        }

        //GetAllClass


        //GetClassDetail

    }
}
