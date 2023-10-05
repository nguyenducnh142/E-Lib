using Microsoft.AspNetCore.Mvc;
using HelpService.Repository;


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


        [HttpPut("SendHelp")]
        public void SendEmail([FromBody]string helpDetail)
        {
            var message = new Message(new string[] { "nguyenducnhat142@gmail.com" }, "Trợ giúp E-Lib", helpDetail);
            _helpRepository.SendEmail(message);
        }
    }
}
