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
        [HttpGet("/GetSystematicInfo")]
        public async Task<IActionResult> GetSystematicInfo()
        {
            var systemInfo = await _leadershipRepository.GetSystemInfo();
            return new OkObjectResult(systemInfo);
        }

        //Add SystemInfo
        [HttpPost("/InsertSystematicInfo")]
        public async Task<IActionResult> AddSystematicInfo(SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            await _leadershipRepository.AddSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }

        //Change System Info
        [HttpPut("/UpdateSystematicInfo")]
        public async Task<IActionResult> UpdateSystematicInfo([FromBody]SystemInfo systemInfo)
        {
            systemInfo.SystemId = "system";
            await _leadershipRepository.UpdateSystemInfo(systemInfo);
            return new OkObjectResult(systemInfo);
        }


        //Search User By Name
        [HttpGet("/GetUserByName/{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = await _leadershipRepository.GetUserByName(userName);
            return new OkObjectResult(user);
        }



        //Add User
        [HttpPost("/AddUser")]
        public async Task<IActionResult> AddUser(Account account)
        {
            await _leadershipRepository.AddUser(account);
            return new OkObjectResult(account);
        }
        

        //Change Role
        [HttpPut("/ChangeUserRole")]
        public async Task<IActionResult> ChangeRole(string userId, string role)
        {
            await _leadershipRepository.ChangeRole(userId, role);
            return new OkObjectResult(userId);
        }

        //Delete User
        [HttpDelete("/DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId) 
        {
            await _leadershipRepository.DeleteUser(userId);
            return new OkObjectResult(userId);
        }

        //Get Users By Role
        [HttpGet("/GetUserByRole/{role}")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            switch (role)
            {
                case "leadership": return new OkObjectResult(GetLeaderships());
                case "teacher": return new OkObjectResult(GetTeachers());
                case "student": return new OkObjectResult(GetStudents());
                default: return new OkObjectResult("Wrong Role");
            }
        }

        //Get All User
        [HttpGet("/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var accounts = await _leadershipRepository.GetUsers();
            return new OkObjectResult(accounts);
        }

        [HttpGet("/GetLeaderships")]
        public async Task<IActionResult> GetLeaderships()
        {
            var accounts = await _leadershipRepository.GetLeaderships();
            return new OkObjectResult(accounts);
        }
        [HttpGet("/GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var accounts = await _leadershipRepository.GetTeachers();
            return new OkObjectResult(accounts);
        }
        [HttpGet("/GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var accounts = await _leadershipRepository.GetStudents();
            return new OkObjectResult(accounts);
        }

        //Add Student Into Class
        [HttpPut("/AddStudentIntoClass")]
        public async Task<IActionResult> AddStudentIntoClass(string userId, string classId)
        {
            _leadershipRepository.AddStudentIntoClass(userId, classId);
            return new OkObjectResult(true);
        }
    }
}
