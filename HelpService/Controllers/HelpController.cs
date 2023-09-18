using Microsoft.AspNetCore.Mvc;
using HelpService.Repository;
using System.Transactions;
using HelpService.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Routing;

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

        [HttpPost("SendHelp")]
        public String SendMail( string helpDetail, SmtpClient client)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: "nguyenducnhat142@gmail.com",
                to: "nguyenducnhat142@gmail.com",
                subject: "Trợ giúp E-Lab",
                body: helpDetail
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress("nguyenducnhat142@gmail.com"));
            message.Sender = new MailAddress("nguyenducnhat142@gmail.com");


            try
            {
                client.SendMailAsync(message);
                return client.Port.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return client.Port.ToString();
            }
        }
    }
}
