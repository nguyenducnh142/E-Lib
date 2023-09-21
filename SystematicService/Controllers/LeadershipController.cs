using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystematicService.Models;
using SystematicService.Repository;

namespace SystematicService.Controllers
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

        [HttpGet("GetAllAccount")]
        public IActionResult GetAllAccount()
        {
            var account = _leadershipRepository.GetAllAccount();
            return new OkObjectResult(account);
        }

        [HttpGet("GetSystematicInfo")]
        public IActionResult GetSystematicInfo()
        {
            var systemInfo = _leadershipRepository.GetSystemInfo();
            return new OkObjectResult(systemInfo);
        }

        [HttpPost("InsertSystematicInfo")]
        public IActionResult AddSystematicInfo(SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            _leadershipRepository.AddSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }

        [HttpPut("UpdateSystematicInfo")]
        public IActionResult UpdateSystematicInfo([FromBody]SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            _leadershipRepository.UpdateSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }

        [HttpGet("GetUserByName")]
        public IActionResult GetUserByName(string userName)
        {
            var user = _leadershipRepository.GetUserByName(userName);
            return new OkObjectResult(user);
        }
    
        [HttpPost("AddUser")]
        public IActionResult AddUser(Account account)
        {
            _leadershipRepository.AddAccount(account); 
            return new OkObjectResult(account);
        }

        [HttpPut("ChangeUserRole")]
        public IActionResult ChangeRole(string userId, int role)
        {
            _leadershipRepository.ChangeRole(userId, role);
            return new OkObjectResult(userId);
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string userId) 
        {
            _leadershipRepository.DeleteUser(userId);
            return new OkObjectResult(userId);
        }

        [HttpGet("GetUserByRole")]
        public IActionResult GetUserByRole(int role)
        {
            var accounts = _leadershipRepository.GetUserByRole(role);
            return new OkObjectResult(accounts);
        }
    }
}
