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

        //UpdateLesson&LessonFile


        //ViewAsStudent???


        //AddLesson


        //AddLessonFile


        //GetAllClass


        //GetClassDetail

    }
}
