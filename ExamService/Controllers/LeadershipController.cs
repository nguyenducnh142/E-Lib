﻿using ExamService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILeadershipRepository _leadershipRepository;

        public LeadershipController(ITeacherRepository teacherRepository, ILeadershipRepository leadershipRepository)
        {
            _teacherRepository = teacherRepository;
            _leadershipRepository = leadershipRepository;
        }

        [HttpPut("ChangeApproveExam")]
        public IActionResult ChangeApproveExam(string examId)
        {
            _leadershipRepository.ChangeApproveExam(examId);
            return new OkObjectResult(examId);
        }

    }
}
