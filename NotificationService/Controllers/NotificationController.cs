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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
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
            var noti = _notificationRepository.GetSubjectNoti();
            return new OkObjectResult(noti);
        }

        //Get Questions Noti
        [HttpGet("GetQuestionNoti")]
        public IActionResult GetQuestionNoti()
        {
            var noti = _notificationRepository.GetQuestionNoti();
            return new OkObjectResult(noti);
        }

        //Get Account Noti
        [HttpGet("GetAccountNoti")]
        public  IActionResult   GetAccountNoti()
        {
            var noti = _notificationRepository.GetAccountNoti(GetUserId());
            return new OkObjectResult(noti);
        }

        //Search Notis
        [HttpGet("SearchNoti")]
        public IActionResult FindNotiByDetail(string notiDetail)
        {
            var notis = _notificationRepository.FindNoti(notiDetail);
            return new OkObjectResult(notis);
        }


        
        //Delete Noti
        [HttpDelete("DeleteNoti")]
        public IActionResult DeleteNoti(int notiId)
        {
            _notificationRepository.DeleteNoti(notiId);
            return new OkObjectResult(notiId);
        }

    }
}
