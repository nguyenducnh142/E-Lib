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

        public LeadershipController(ILeadershipRepository leadershipRepository)
        {
            _leadershipRepository = leadershipRepository;
        }
        
        //Get Subjects
        [HttpGet("/GetSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _leadershipRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        //Get SubjectNonApprove
        [HttpGet("/GetSubjectNonAprove")]
        public async Task<IActionResult> GetSubjectNonAproved()
        {
            var subjects = await _leadershipRepository.GetSubjectsNonAproved();
            return new OkObjectResult(subjects);
        }

        //Get Lessons By SubjectId
        [HttpGet("/GetLessons/{subjectId}")]
        public async Task<IActionResult> GetLessonList(string subjectId)
        {
            var lesson = await _leadershipRepository.GetLessons(subjectId);
            return new OkObjectResult(lesson);
        }

        //Get LessonFiles
        [HttpGet("/GetLessonFiles")]
        public async Task<IActionResult> GetLessonFiles()
        {
            var lessonFile = await _leadershipRepository.GetLessonFiles();
            return new OkObjectResult(lessonFile);
        }

        //Get LessonFiles Non Aproved
        [HttpGet("/GetLessonFilesNonApproved")]
        public async Task<IActionResult> GetLessonFileNonAprovedList()
        {
            var lessonFile = await _leadershipRepository.GetLessonFilesNonAproved();
            return new OkObjectResult(lessonFile);
        }

        //Sort LessonFiles
        [HttpGet("/SortLessonFile/{sortby}")]
        public async Task<IActionResult> GetLessonFileListSorted(string sortby)
        {
            var lessonFile = await _leadershipRepository.SortLessonFiles(sortby);
            return new OkObjectResult(lessonFile);
        }



        //Aprove/UnAprove LessonFile
        [HttpPut("/AproveLessonFile/{lessonFileId}")]
        public async Task<IActionResult> AproveLessonFile(string lessonFileId)
        {
            using (var scope = new TransactionScope())
            {
                await _leadershipRepository.AproveLessonFile(lessonFileId);
                scope.Complete();
                return new OkResult();
            }
        }


        //Search LessonFile
        [HttpGet("/SearchLessonFile/{lessonFileName}")]
        public async Task<IActionResult> GetLessonFileByName(string lessonFileName)
        {
            var lessonFile = await _leadershipRepository.GetLessonFileByName(lessonFileName);
            return new OkObjectResult(lessonFile);
        }

        //Add Subject
        [HttpPost("/AddSubject")]
        public async Task<IActionResult> AddSubject( Subject subject)
        {
            using (var scope = new TransactionScope())
            {
                await _leadershipRepository.InsertSubject(subject);
                scope.Complete();
                return CreatedAtAction(nameof(GetSubjects), new { id = subject.SubjectId }, subject);
            }
        }
    }
}
