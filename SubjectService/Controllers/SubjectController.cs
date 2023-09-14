﻿using Microsoft.AspNetCore.Mvc;
using SubjectService.Models;
using SubjectService.Repository;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        // GET: api/<SubjectController>
        [HttpGet]
        public IActionResult Get()
        {
            var subjects = _subjectRepository.GetSubjects();
            return new OkObjectResult(subjects);
        }

        // GET api/<SubjectController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var subject = _subjectRepository.GetSubjectByID(id);
            return new OkObjectResult(subject);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public IActionResult Post([FromBody] Subject subject)
        {
            using (var scope = new TransactionScope())
            {
                _subjectRepository.InsertSubject(subject);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = subject.SubjectId }, subject);
            }
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Subject subject)
        {
            if (subject != null)
            {
                using (var scope = new TransactionScope())
                {
                    _subjectRepository.UpdateSubject(subject);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subjectRepository.DeleteSubject(id);
            return new OkResult();
        }
    }
}