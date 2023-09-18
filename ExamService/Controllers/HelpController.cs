using Microsoft.AspNetCore.Mvc;
using HelpService.Repository;
using System.Transactions;
using HelpService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHelpRepository _helpRepository;

        public HelpController(IHelpRepository helpRepository)
        {
            _helpRepository = helpRepository;
        }


        [HttpPost("AddHelp")]
        public IActionResult SendHelp([FromBody] string helpDetail)
        {
            using (var scope = new TransactionScope())
            {
                _helpRepository.SendHelp(helpDetail);
                scope.Complete();
                return Ok(helpDetail);
            }
        }
    }
}
