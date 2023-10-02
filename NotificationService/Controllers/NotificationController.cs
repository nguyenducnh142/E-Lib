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
        [HttpGet("/GetSubjectNoti")]
        public async Task<IActionResult> GetSubjectNoti()
        {
            var noti = await _notificationRepository.GetSubjectNoti();
            return new OkObjectResult(noti);
        }

        //Get Questions Noti
        [HttpGet("/GetQuestionNoti")]
        public async Task<IActionResult> GetQuestionNoti()
        {
            var noti = await _notificationRepository.GetQuestionNoti();
            return new OkObjectResult(noti);
        }

        //Get Account Noti
        [HttpGet("/GetAccountNoti")]
        public async Task<IActionResult> GetAccountNoti()
        {
            var noti = await _notificationRepository.GetAccountNoti(GetUserId());
            return new OkObjectResult(noti);
        }

        //Search Notis
        [HttpGet("/SearchNoti/{notiDetail}")]
        public async Task<IActionResult> FindNotiByDetail(string notiDetail)
        {
            var notis = await _notificationRepository.FindNoti(notiDetail);
            return new OkObjectResult(notis);
        }


        //Add Noti (System)
        [HttpPost("/AddNoti")]
        public async Task<IActionResult> AddNoti(string subjectId, string notiDetail)
        {
            await _notificationRepository.AddNoti(subjectId, notiDetail);
            return new OkObjectResult(subjectId);
        }
        [HttpPost("/AddUserNoti")]
        public async Task<IActionResult> AddPersonalNoti(string userId, string notiDetail)
        {
            await _notificationRepository.AddPersonalNoti(userId, notiDetail);
            return new OkObjectResult(userId);
        }

        //Delete Noti
        [HttpDelete("/DeleteNoti")]
        public async Task<IActionResult> DeleteNoti(int notiId)
        {
            await _notificationRepository.DeleteNoti(notiId);
            return new OkObjectResult(notiId);
        }

    }
}
