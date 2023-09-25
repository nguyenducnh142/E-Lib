using HelpService.DBContexts;
using HelpService.Model;
using HelpService.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HelpService.Repository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly HelpContext _dbContext;
        private readonly EmailConfiguration _emailConfig;
        public HelpRepository(HelpContext helpContext, EmailConfiguration emailConfig)
        {
            _dbContext = helpContext;
            _emailConfig = emailConfig;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void SendHelp(Help help)
        {
            _dbContext.Add(help);
            Save();

        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From,_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
