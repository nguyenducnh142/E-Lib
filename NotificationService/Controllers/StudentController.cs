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
    public class StudentNotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentNotificationController(INotificationRepository notificationRepository, IStudentRepository studentRepository)
        {
            _notificationRepository = notificationRepository;
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
        public async Task<IActionResult> GetSubjectNoti()
        {
            var noti = await _studentRepository.GetSubjectNoti(GetUserId());
            return new OkObjectResult(noti);
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
        [HttpGet("SearchNotification/{notiDetail}")]
        public IActionResult FindNotiByDetail(string notiDetail)
        {
            var noti = _studentRepository.FindNoti(notiDetail, GetUserId());
            return new OkObjectResult(noti);
        }


    }
}
