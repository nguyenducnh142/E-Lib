using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Repository;

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

        //Xem thông báo môn học.
        [HttpGet("GetSubjectNoti")]
        public IActionResult GetSubjectNoti()
        {
            var noti = _notificationRepository.GetSubjectNoti();
            return new OkObjectResult(noti);
        }

        //Xem thông báo câu hỏi
        [HttpGet("GetQuestionNoti")]
        public IActionResult GetQuestionNoti()
        {
            var noti = _notificationRepository.GetQuestionNoti();
            return new OkObjectResult(noti);
        }

        //Xem thông báo thông tin tài khoản 
        [HttpGet("GetAccountNoti")]
        public IActionResult GetAccountNoti(string userId)
        {
            var noti = _notificationRepository.GetAccountNoti();
            return new OkObjectResult(noti);
        }

        //Tìm kiếm thông báo theo nội dung 
        [HttpGet]
        public IActionResult FindNotiByDetail(string notiDetail)
        {
            var noti = _notificationRepository.FindNoti(notiDetail);
            return new OkObjectResult(noti);
        }

        //Xem thông báo tất cả môn học (api gateway)

        //Thêm thông báo (hệ thống)
        [HttpPost]
        public IActionResult AddNoti(Notification notification)
        {
            _notificationRepository.AddNoti(notification);
            return new OkObjectResult(notification);
        }
        [HttpPost]
        public IActionResult AddPersonalNoti(PersonalNotification notification)
        {
            _notificationRepository.AddPersonalNoti(notification);
            return new OkObjectResult(notification);
        }

        //Xóa thông báo
        [HttpDelete]
        public IActionResult DeleteNoti(string notiId)
        {
            _notificationRepository.DeleteNoti(notiId);
            return new OkObjectResult(notiId);
        }

    }
}
