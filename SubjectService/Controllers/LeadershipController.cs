using Microsoft.AspNetCore.Mvc;
//using SubjectService.Migrations;
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
        private readonly ILeadershipRepository _leadershipRepository;
        private readonly NotificationService.Repository.INotificationRepository _notificationRepository;

        public LeadershipController(ILeadershipRepository leadershipRepository, NotificationService.Repository.INotificationRepository notificationRepository = null)
        {
            _leadershipRepository = leadershipRepository;
            _notificationRepository = notificationRepository;
        }

        //Get Subjects
        [HttpGet("GetSubjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _leadershipRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        //Get SubjectNonApprove
        [HttpGet("GetSubjectNonAprove")]
        public IActionResult GetSubjectNonAproved()
        {
            var subjects =  _leadershipRepository.GetSubjectsNonAproved();
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("GetLessons")]
        public IActionResult GetLessonList(string subjectId)
        {
            var lesson =_leadershipRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFiles
        [HttpGet("GetLessonFiles")]
        public IActionResult GetLessonFiles()
        {
            var lessonFile =_leadershipRepository.GetLessonFiles();
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFiles Non Aproved
        [HttpGet("GetLessonFilesNonApproved")]
        public IActionResult GetLessonFileNonAprovedList()
        {
            var lessonFile = _leadershipRepository.GetLessonFilesNonAproved();
            return new OkObjectResult(lessonFile);
        }

        //Sort LessonFiles
        [HttpGet("SortLessonFile/{sortby}")]
        public IActionResult GetLessonFileListSorted(string sortby)
        {
            var lessonFile = _leadershipRepository.SortLessonFiles(sortby);
            return new OkObjectResult(lessonFile);
        }



        //Aprove/UnAprove LessonFile
        [HttpPut("AproveLessonFile")]
        public IActionResult AproveLessonFile(string lessonFileId)
        {
            using (var scope = new TransactionScope())
            {
                _leadershipRepository.AproveLessonFile(lessonFileId);
                _notificationRepository.AddNoti(GetSubjectId(lessonFileId), "Môn học" + GetSubjectId(lessonFileId) + "của bạn đã được thêm tài liệu mới");
                scope.Complete();
                return new OkResult();
            }
            
        }

        //Get SubjectId by LessonFileId
        private string GetSubjectId(string lessonFileId)
        {
            return _leadershipRepository.GetSubjectId(lessonFileId);
        }
        
        //Search LessonFile
        [HttpGet("SearchLessonFile")]
        public IActionResult GetLessonFileByName(string lessonFileName)
        {
            var lessonFile = _leadershipRepository.GetLessonFileByName(lessonFileName);
            return new OkObjectResult(lessonFile);
        }

        //Add Subject
        [HttpPost("AddSubject")]
        public IActionResult AddSubject( Subject subject)
        {
            using (var scope = new TransactionScope())
            {
                _leadershipRepository.InsertSubject(subject);
                _notificationRepository.AddNoti(subject.SubjectId, "Môn học" + subject.SubjectName + "đã được thêm");
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = subject.SubjectId }, subject);
            }
        }

        //Add Subject Into Class
        [HttpPut("AddSubjectIntoClass")]
        public IActionResult AddSubjectIntoClass(SubjectClass subjectClass)
        {
            _leadershipRepository.AddSubjectIntoClass(subjectClass.SubjectId,subjectClass.ClassId);
            return new OkObjectResult(" Thêm thành công môn học "+subjectClass.SubjectId+" vào lớp "+subjectClass.ClassId);
        }
    }
}
