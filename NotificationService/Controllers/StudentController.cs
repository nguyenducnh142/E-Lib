using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Repository;
using System.Security.Claims;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController( IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        //Get Current UserDetail
        private string GetUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("name"));
            return id;
        }

        //Get Subjects Noti
        [HttpGet("GetSubjectNoti")]
        public IActionResult GetSubjectNoti()
        {
            var notis = _studentRepository.GetSubjectNoti(GetUserId());
            return new OkObjectResult(notis);
        }

        //Get Questions Noti
        [HttpGet("GetQuestionNoti")]
        public IActionResult GetQuestionNoti()
        {
            var noti = _studentRepository.GetQuestionNoti(GetUserId());
            return new OkObjectResult(noti);
        }

        //Get Account Noti
        [HttpGet("GetAccountNoti")]
        public IActionResult GetAccountNoti()
        {
            var noti = _studentRepository.GetAccountNoti(GetUserId());
            return new OkObjectResult(noti);
        }

        //Search Noti
        [HttpGet("SearchNotification")]
        public IActionResult FindNotiByDetail(string notiDetail)
        {
            var noti = _studentRepository.FindNoti(notiDetail, GetUserId());
            return new OkObjectResult(noti);
        }


    }
}
