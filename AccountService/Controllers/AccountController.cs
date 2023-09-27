using AccountService.Models;
using AccountService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Transactions;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("/GetAccountDetail/{accountId}")]
        public IActionResult GetAccountDetail(string  accountId)
        {
            var account = _accountRepository.GetAccountDetail(accountId);
            return new OkObjectResult(account);
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount(Account account)
        {
            _accountRepository.InsertAccount(account);
            return Ok();
        
        }

        [HttpGet("GetAvatar")]
        public IActionResult GetPicture(string userId)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", userId);
            if (!System.IO.File.Exists(filepath))
            {
                return File(System.IO.File.OpenRead("Upload\\Files\\default.jpg"), "image/jpeg");
            }
            return File(System.IO.File.OpenRead(filepath), "image/jpeg");
        }

        [HttpPost("UploadAva")]
        public IActionResult UploadFile(IFormFile file, string userId, CancellationToken cancellationtoken)
        {
            return Ok(_accountRepository.WriteFile(file, userId));
        }

        [HttpDelete("DeleteAva")]
        public IActionResult DeleteAvatar(string fileName)
        {
            _accountRepository.DeleteAvatar(fileName);
            return new OkResult();
        }

        [HttpPut("ChangePassword")]
        public string ChangePassword (string userId, string oldPassword, string newPassword)
        {
            return _accountRepository.ChangePassword(userId, oldPassword, newPassword);
            
        }

       
    }
}
