using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
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
        


        //Get System Info
        [HttpGet("GetSystematicInfo")]
        public IActionResult GetSystematicInfo()
        {
            var systemInfo = _leadershipRepository.GetSystemInfo();
            return new OkObjectResult(systemInfo);
        }

        //Add SystemInfo
        [HttpPost("InsertSystematicInfo")]
        public IActionResult AddSystematicInfo(SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            _leadershipRepository.AddSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }

        //Change System Info
        [HttpPut("UpdateSystematicInfo")]
        public IActionResult UpdateSystematicInfo([FromBody]SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            _leadershipRepository.UpdateSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }


        //Search User By Name
        [HttpGet("GetUserByName")]
        public IActionResult GetUserByName(string userName)
        {
            var user = _leadershipRepository.GetUserByName(userName);
            return new OkObjectResult(user);
        }



        //Add User
        [HttpPost("AddUser")]
        public IActionResult AddUser(Account account)
        {
            _leadershipRepository.AddUser(account);
            return new OkObjectResult(account);
        }
        

        //Change Role
        [HttpPut("ChangeUserRole")]
        public IActionResult ChangeRole(string userId, string role)
        {
            _leadershipRepository.ChangeRole(userId, role);
            return new OkObjectResult(userId);
        }

        //Delete User
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string userId) 
        {
            _leadershipRepository.DeleteUser(userId);
            return new OkObjectResult(userId);
        }

        //Get Users By Role
        [HttpGet("GetUserByRole")]
        public IActionResult GetUsersByRole(string role)
        {
            switch (role)
            {
                case "leadership":
                    return new OkObjectResult(_leadershipRepository.GetLeaderships());
                case "teacher":
                    return new OkObjectResult(_leadershipRepository.GetTeachers());
                case "student":
                    return new OkObjectResult( _leadershipRepository.GetStudents());
                default: return new OkObjectResult("Wrong Role");
            }
        }

        //Get All User
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var accounts = _leadershipRepository.GetUsers();
            return new OkObjectResult(accounts);
        }

        

        //Add Student Into Class
        [HttpPut("AddStudentIntoClass")]
        public IActionResult AddStudentIntoClass(string userId, string classId)
        {
            _leadershipRepository.AddStudentIntoClass(userId, classId);
            return new OkObjectResult(true);
        }
    }
}
