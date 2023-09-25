
using HelpService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpService.Repository
{
    public interface IHelpRepository
    {
        void SendHelp(Help help);
        void SendEmail(Message message);
    }
}
