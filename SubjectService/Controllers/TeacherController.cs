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
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }


        //Get Subjects 
        [HttpGet("/AllSubject")]
        public async Task<IActionResult> GetSubjects(string teacherName)
        {
            var subjects = await _teacherRepository.GetSubjects(teacherName);
            return new OkObjectResult(subjects);
        }

        //Search Subjects By SearchInfo(subjectId/SubjectName)
        [HttpGet("/SearchSubject/{searchInfo}")]
        public async Task<IActionResult> SearchSubjects(string searchInfo)
        {
            var subjects = await _teacherRepository.SearchSubjects(searchInfo);
            return new OkObjectResult(subjects);
        }

        //Sort Subject By Name
        [HttpGet("/SortSubjects")]
        public async Task<IActionResult> SortSubjects(string teacherName)
        {
            var subjects = await _teacherRepository.SortedSubjects(teacherName);
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("/GetLessons/{subjectId}")]
        public async Task<IActionResult> GetLessons(string subjectId)
        {
            var lesson = await _teacherRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFile By LessonId
        [HttpGet("/GetLessonFiles/{lessonId}")]
        public async Task<IActionResult> GetLessonFiles(string lessonId)
        {
            var lessonFile = await _teacherRepository.GetLessonFilesByLesson(lessonId);
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFile By SubjectId
        [HttpGet("/GetLessonFiles/{subjectId}")]
        public async Task<IActionResult> GetLessonFilesBySubject(string subjectId)
        {
            var lessonFile = await _teacherRepository.GetLessonFilesBySubject(subjectId);
            return new OkObjectResult(lessonFile);
        }

        //Change SubjectDescription
        [HttpPut("/UpdateSubjectDescription")]
        public async Task<IActionResult> UpdateSubDescrip(string subjectDecription, string subjectId)
        {
            if (subjectDecription != null && subjectId != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _teacherRepository.UpdateSubDes(subjectDecription, subjectId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        //Change LessonName
        [HttpPut("/UpdateLessonName")]
        public async Task<IActionResult> UpdateLesson(string lessonName, string lessonId)
        {
            if (lessonName != null && lessonId != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _teacherRepository.UpdateLesson(lessonName, lessonId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        //Delete LessonFile (Filename + đuôi)
        [HttpDelete("/DeleteLessonFile")]
        public async Task<IActionResult> DeleteFile(string lessonFileName)
        {
            await _teacherRepository.DeleteLessonFile(lessonFileName);
            return new OkResult();
        }

        //Update LessonFileDesciption
        [HttpPut("/UpdateLessonFileName")]
        public async Task<IActionResult> UpdateLessonFileName(string lessonFileName, string lessonFileId)
        {
            if (lessonFileName != null && lessonFileId != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _teacherRepository.UpdateLessonFile(lessonFileName, lessonFileId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        //AddLesson
        [HttpPost("/AddLesson")]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            using (var scope = new TransactionScope())
            {
                await _teacherRepository.InsertLesson(lesson);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = lesson.LessonId }, lesson);
            }
        }

        //Add LessonFile
        [HttpPost("/UploadLessonFile")]
        public async Task<IActionResult> UploadLessonFile(IFormFile file, string lessonFileName, string lessonId, string lessonDescription)
        {
            await _teacherRepository.WriteFile(file, lessonFileName, lessonId, lessonDescription);
            return new OkObjectResult(lessonFileName);
        }


        //Get Class
        [HttpGet("/AllSubject")]
        public async Task<IActionResult> GetClass(string teacherName)
        {
            var subjects = await _teacherRepository.GetClass(teacherName);
            return new OkObjectResult(subjects);
        }

        //Add Subject Notification
        [HttpPost("/AddSubjectNotification")]
        public async Task<IActionResult> AddNotification( SubjectNotification subjectNotification)
        {
            using (var scope = new TransactionScope())
            {
                await _teacherRepository.InsertSubjectNoti(subjectNotification);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = subjectNotification.SubjectNotificationId }, subjectNotification);
            }
        }
    }
}
