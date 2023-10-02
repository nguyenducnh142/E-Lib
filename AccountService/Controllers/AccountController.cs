using AccountService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NotificationService;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly NotificationService.Repository.INotificationRepository _notificationRepository;

        public AccountController(IAccountRepository accountRepository, NotificationService.Repository.INotificationRepository notificationRepository)
        {
            _accountRepository = accountRepository;
            _notificationRepository = notificationRepository;
        }

        //Get Current UserDetail
        private string GetUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("name"));
            return id;
        }
        private string GetUserRole()
        {
            string role = Convert.ToString(HttpContext.User.FindFirstValue("Role"));
            return role;
        }

        //Get Current User Info
        [HttpGet("GetUserDetail")]
        public IActionResult GetAccountDetail()
        {
            
             return new OkObjectResult( _accountRepository.GetAccount(GetUserId()));
        }



        //Get CurrentUser Avatar
        [HttpGet("GetAvatar")]
        public IActionResult GetPicture()
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", GetUserId());
            if (!System.IO.File.Exists(filepath))
            {
                return File(System.IO.File.OpenRead("Upload\\Files\\default.jpg"), "image/jpeg");
            }
            return File(System.IO.File.OpenRead(filepath), "image/jpeg");
        }

        //Add Avatar
        [HttpPost("UploadAva")]
        public IActionResult UploadFile(IFormFile file, CancellationToken cancellationtoken)
        {
            return Ok(_accountRepository.WriteFile(file, GetUserId()));
        }

        //Delete Avatar
        [HttpDelete("DeleteAva")]
        public IActionResult DeleteAvatar()
        {
            _accountRepository.DeleteAvatar(GetUserId()+".jpg");
            return new OkResult();
        }

        //Change Password
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword (string oldPassword, string newPassword)
        {
            var status = _accountRepository.ChangePassword(GetUserId(), oldPassword, newPassword);
            _notificationRepository.AddPersonalNoti(GetUserId(), "Tài Khoản của bạn đã được thay đổi mật khẩu");
            return new OkObjectResult(status);
        }

       
    }
}
